#define IRB6700
using ActUtlTypeLib; /* The utility setting type control which is used to create a user
                        program using Communication Setup Utility. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net.Sockets;



/**
 * Author: Gabriele Marini (Gabryxx7)
 * This class load the 3d models of all the parts of the robotic arms and add them to the viewport
 * It also defines the relations among the joints of the robotic arms in order to reflect the movement of the robot in the real world
 * **/
namespace RobotArmHelix
{
    class Joint
    {
        public Model3D model = null;
        public double angle = 0;
        public double angleMin = -180;
        public double angleMax = 180;
        public int rotPointX = 0;
        public int rotPointY = 0;
        public int rotPointZ = 0;
        public int rotAxisX = 0;
        public int rotAxisY = 0;
        public int rotAxisZ = 0;

        public Joint(Model3D pModel)
        {
            model = pModel;
        }
    }

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MainWindow : Window
   {

        private byte[,] array2D;
        //Declaration for connecting TCP/IP
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        //provides functionality to 3d models
        Model3DGroup RA = new Model3DGroup(); //RoboticArm 3d group
        Model3D geom = null; //Debug sphere to check in which point the joint is rotatin
        public ActUtlType plc = new();
        List<Joint> joints = null;
        int move = 0; /* move = 1 -> MoveJ, move = 2 -> MoveL */

        bool switchingJoint = false;
        bool isAnimating = false;

        public double joint1_value, joint2_value, joint3_value, joint4_value, joint5_value;

        public double[] angles_global = {0, 0, 0, 0, 0};

        public bool cn_bttn = true;
        public bool ds_bttn = false;
        public bool testpos_bttn = true;

        Color oldColor = Colors.White;
        GeometryModel3D oldSelectedModel = null;
        string basePath = "";
        ModelVisual3D visual;
        double LearningRate = 0.01;
        double SamplingDistance = 0.15;
        double DistanceThreshold = 20;
        //provides render to model3d objects
        ModelVisual3D RoboticArm = new ModelVisual3D();
        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F4;
        Transform3DGroup F5;
        Transform3DGroup F6;
        RotateTransform3D R;
        TranslateTransform3D T;
        Vector3D reachingPoint;
        int movements = 10;
        System.Windows.Forms.Timer timer1;
        System.Windows.Forms.Timer timer2;
#if IRB6700
        //directroy of all stl files
        private const string MODEL_PATH0 = "K0.stl";
        private const string MODEL_PATH1 = "K1.stl";
        private const string MODEL_PATH2 = "K2.stl";
        private const string MODEL_PATH3 = "K3.stl";
        private const string MODEL_PATH4 = "K4.stl";
        private const string MODEL_PATH5 = "K5.stl";
#endif


        public MainWindow()
        {
            InitializeComponent();
            // Tạo và bắt đầu một luồng mới
            Thread thread1 = new Thread(new ThreadStart(Task1));
            thread1.Start();

            // Attach the event handler to the MouseDown event
            viewPort3d.MouseDown += helixViewport3D_MouseDown;
            basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\3D_Models\\";
            List<string> modelsNames = new List<string>();
            modelsNames.Add(MODEL_PATH0);
            modelsNames.Add(MODEL_PATH1);
            modelsNames.Add(MODEL_PATH2);
            modelsNames.Add(MODEL_PATH3);
            modelsNames.Add(MODEL_PATH4);
            modelsNames.Add(MODEL_PATH5);

#if IRB6700
#endif
            RoboticArm.Content = Initialize_Environment(modelsNames);

            /** Debug sphere to check in which point the joint is rotating**/
            var builder = new MeshBuilder(true, true);
            var position = new Point3D(0, 0, 0);
            /* Create red sphere */
            builder.AddSphere(position, 50, 15, 15);
            geom = new GeometryModel3D(builder.ToMesh(), Materials.Brown);
            visual = new ModelVisual3D();
            visual.Content = geom;

            viewPort3d.RotateGesture = new MouseGesture(MouseAction.RightClick);
            viewPort3d.PanGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort3d.Children.Add(visual);
            viewPort3d.Children.Add(RoboticArm);
            viewPort3d.Camera.LookDirection = new Vector3D(1948, -4375, -4105);
            viewPort3d.Camera.UpDirection = new Vector3D(-0.031, 0.07, 0.997);
            viewPort3d.Camera.Position = new Point3D(-567, 4895, 4620);

            double[] angles = { joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
            ForwardKinematics(angles);


            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 40;
            timer1.Tick += new System.EventHandler(timer1_Tick);

            timer2 = new System.Windows.Forms.Timer();
            timer2.Interval = 5000;
            timer2.Tick += new System.EventHandler(timer2_Tick);

        }

        public void Task1()
        {

            // Sử dụng Dispatcher để thay đổi UI element từ một luồng khác
            Dispatcher.Invoke(() =>
            {
                joint1.Value = angles_global[0];
                joint2.Value = angles_global[1];
                joint3.Value = angles_global[2];
                joint4.Value = angles_global[3];
                joint5.Value = angles_global[4];
            });

        }
        private Model3DGroup Initialize_Environment(List<string> modelsNames)
        {
            try
            {
                ModelImporter import = new ModelImporter();
                joints = new List<Joint>();

                foreach(string modelName in modelsNames)
                {
                    var materialGroup = new MaterialGroup();
                    Color mainColor = Colors.White;
                    EmissiveMaterial emissMat = new EmissiveMaterial(new SolidColorBrush(mainColor));
                    DiffuseMaterial diffMat = new DiffuseMaterial(new SolidColorBrush(mainColor));
                    SpecularMaterial specMat = new SpecularMaterial(new SolidColorBrush(mainColor), 200);
                    materialGroup.Children.Add(emissMat);
                    materialGroup.Children.Add(diffMat);
                    materialGroup.Children.Add(specMat);

                    var link = import.Load(basePath + modelName);
                    GeometryModel3D model = link.Children[0] as GeometryModel3D;
                    model.Material = materialGroup;
                    model.BackMaterial = materialGroup;
                    joints.Add(new Joint(link));

                }
                RA.Children.Add(joints[0].model);
                RA.Children.Add(joints[1].model);
                RA.Children.Add(joints[2].model);
                RA.Children.Add(joints[3].model);
                RA.Children.Add(joints[4].model);
                RA.Children.Add(joints[5].model);
#if IRB6700

#endif

#if IRB6700
                changeModelColor(joints[0], Colors.Black);
                changeModelColor(joints[1], Colors.OrangeRed);
                changeModelColor(joints[2], Colors.OrangeRed);
                changeModelColor(joints[3], Colors.OrangeRed);
                changeModelColor(joints[4], Colors.OrangeRed);
                changeModelColor(joints[5], Colors.Tomato);

                joints[1].angleMin = -180;
                joints[1].angleMax = 180;
                joints[1].rotAxisX = 0;
                joints[1].rotAxisY = 0;
                joints[1].rotAxisZ = 1;
                joints[1].rotPointX = 170;
                joints[1].rotPointY = 355;
                joints[1].rotPointZ = 0;

                joints[2].angleMin = -100;
                joints[2].angleMax = 60;
                joints[2].rotAxisX = 0;
                joints[2].rotAxisY = 1;
                joints[2].rotAxisZ = 0;
                joints[2].rotPointX = 169;
                joints[2].rotPointY = 0;
                joints[2].rotPointZ = 1060;

                joints[3].angleMin = -90;
                joints[3].angleMax = 90;
                joints[3].rotAxisX = 0;
                joints[3].rotAxisY = 1;
                joints[3].rotAxisZ = 0;
                joints[3].rotPointX = 609;
                joints[3].rotPointY = 0;
                joints[3].rotPointZ = 1060;

                joints[4].angleMin = -180;
                joints[4].angleMax = 180;
                joints[4].rotAxisX = 0;
                joints[4].rotAxisY = 1;
                joints[4].rotAxisZ = 0;
                joints[4].rotPointX = 1112;
                joints[4].rotPointY = 0;
                joints[4].rotPointZ = 1061;

                joints[5].angleMin = -115;
                joints[5].angleMax = 115;
                joints[5].rotAxisX = 1;
                joints[5].rotAxisY = 0;
                joints[5].rotAxisZ = 0;
                joints[5].rotPointX = 0;
                joints[5].rotPointY = 350;
                joints[5].rotPointZ = 1062;

#else

#endif
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
        }

        public static T Clamp<T>(T value, T min, T max)
            where T : System.IComparable<T>
                {
                    T result = value;
                    if (value.CompareTo(max) > 0)
                        result = max;
                    if (value.CompareTo(min) < 0)
                        result = min;
                    return result;
                }

        private void ReachingPoint_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                reachingPoint = new Vector3D(Double.Parse(TbX.Text), Double.Parse(TbY.Text), Double.Parse(TbZ.Text));
                //geom.Transform = new TranslateTransform3D(reachingPoint);
            }
            catch (Exception exc)
            {

            }
        }


        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isAnimating)
                return;
            if(cn_bttn == false)
            {
                joints[1].angle = angles_global[0];
                joints[2].angle = angles_global[1];
                joints[3].angle = angles_global[2];
                joints[4].angle = angles_global[3];
                joints[5].angle = angles_global[4];
            }
            else
            {
                joints[1].angle = joint1.Value;
                joints[2].angle = joint2.Value;
                joints[3].angle = joint3.Value;
                joints[4].angle = joint4.Value;
                joints[5].angle = joint5.Value;
            }
            execute_fk();
        }



        /**
         * This methodes execute the FK (Forward Kinematics). It starts from the first joint, the base.
         * */
        private void execute_fk()
        {
            int[] value_positon = new int[16];
            uint t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0;
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            /** Debug sphere, it takes the x,y,z of the textBoxes and update its position
             * This is useful when using x,y,z in the "new Point3D(x,y,z)* when defining a new RotateTransform3D() to check where the joints is actually  rotating */
            if (cn_bttn == true)
            {
                double[] angles = { joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
                ForwardKinematics(angles);
                joint1.Value = angles[0];
                joint2.Value = angles[1];
                joint3.Value = angles[2];
                joint4.Value = angles[3];
                joint5.Value = angles[4];
            }
            else
            {
                /* Read position of 5 angle */
                int[] temp_angle = new int[90];
                plc.ReadDeviceBlock(Constants.R_POSITION_1, 82, out temp_angle[0]);

                value_positon[0] = temp_angle[0];
                value_positon[1] = temp_angle[1];

                value_positon[2] = temp_angle[20];
                value_positon[3] = temp_angle[21];

                value_positon[4] = temp_angle[40];
                value_positon[5] = temp_angle[41];

                value_positon[6] = temp_angle[60];
                value_positon[7] = temp_angle[61];

                value_positon[8] = temp_angle[80];
                value_positon[9] = temp_angle[81];




                // Read and convert driver angle value to real position value (was subtracted by 180)
                t1 = Read_Position((uint)value_positon[0], (uint)value_positon[1]);
                t2 = Read_Position((uint)value_positon[2], (uint)value_positon[3]);
                t3 = Read_Position((uint)value_positon[4], (uint)value_positon[5]);
                t4 = Read_Position((uint)value_positon[6], (uint)value_positon[7]);
                t5 = Read_Position((uint)value_positon[8], (uint)value_positon[9]);

                // Convert theta read from int to double
                t1_out = double.Parse(Convert.ToString((int)t1)) / 100000.0;
                t2_out = double.Parse(Convert.ToString((int)t2)) / 100000.0;
                t3_out = double.Parse(Convert.ToString((int)t3)) / 100000.0;
                t4_out = double.Parse(Convert.ToString((int)t4)) / 100000.0;
                t5_out = double.Parse(Convert.ToString((int)t5)) / 100000.0;

                angles_global[0] = t1_out;
                angles_global[1] = t2_out;
                angles_global[2] = t3_out;
                angles_global[3] = t4_out;
                angles_global[4] = t5_out;
                ForwardKinematics(angles_global);
            }



        }

        private Color changeModelColor(Joint pJoint, Color newColor)
        {
            Model3DGroup models = ((Model3DGroup)pJoint.model);
            return changeModelColor(models.Children[0] as GeometryModel3D, newColor);
        }

        private Color changeModelColor(GeometryModel3D pModel, Color newColor)
        {
            if (pModel == null)
                return oldColor;

            Color previousColor = Colors.Black;

            MaterialGroup mg = (MaterialGroup)pModel.Material;
            if (mg.Children.Count > 0)
            {
                try
                {
                    previousColor = ((EmissiveMaterial)mg.Children[0]).Color;
                    ((EmissiveMaterial)mg.Children[0]).Color = newColor;
                    ((DiffuseMaterial)mg.Children[1]).Color = newColor;
                }
                catch (Exception exc)
                {
                    previousColor = oldColor;
                }
            }

            return previousColor;
        }


        private void selectModel(Model3D pModel)
        {
            try
            {
                Model3DGroup models = ((Model3DGroup) pModel);
                oldSelectedModel = models.Children[0] as GeometryModel3D;
            }
            catch (Exception exc)
            {
                oldSelectedModel = (GeometryModel3D) pModel;
            }
            oldColor = changeModelColor(oldSelectedModel, ColorHelper.HexToColor("#ff3333"));
        }

        private void unselectModel()
        {
            changeModelColor(oldSelectedModel, oldColor);
        }

        private void ViewPort3D_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           Point mousePos = e.GetPosition(viewPort3d);
           PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
           VisualTreeHelper.HitTest(viewPort3d, null, ResultCallback, hitParams);
        }

        private void ViewPort3D_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Perform the hit test on the mouse's position relative to the viewport.
            HitTestResult result = VisualTreeHelper.HitTest(viewPort3d, e.GetPosition(viewPort3d));
            RayMeshGeometry3DHitTestResult mesh_result = result as RayMeshGeometry3DHitTestResult;

            if (oldSelectedModel != null)
                unselectModel();

            if (mesh_result != null)
            {
                selectModel(mesh_result.ModelHit);
            }
        }

        public HitTestResultBehavior ResultCallback(HitTestResult result)
        {
            // Did we hit 3D?
            RayHitTestResult rayResult = result as RayHitTestResult;
            if (rayResult != null)
            {
                // Did we hit a MeshGeometry3D?
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;
                geom.Transform = new TranslateTransform3D(new Vector3D(rayResult.PointHit.X, rayResult.PointHit.Y, rayResult.PointHit.Z));

                if (rayMeshResult != null)
                {
                    // Yes we did!
                }
            }

            return HitTestResultBehavior.Continue;
        }

        //public void StartInverseKinematics(object sender, RoutedEventArgs e)
        //{
        //    double x, y, z;
        //    double[] angles = { joints[1].angle, joints[2].angle - 90.0, joints[3].angle + 90.0, joints[4].angle + 90.0, joints[5].angle };

        //    x = Double.Parse(TbX.Text);
        //    y = Double.Parse(TbY.Text);
        //    z = Double.Parse(TbZ.Text);

        //    // Assuming convert_position_angle returns a tuple of 5 values
        //    (double angle1, double angle2, double angle3, double angle4, double angle5) = convert_position_angle(x, y, z);

        //    angles[0] = angle1;
        //    angles[1] = angle2 - 90.0;
        //    angles[2] = angle3 + 90.0;
        //    angles[3] = angle4 + 90.0;
        //    angles[4] = angle5;

        //    joint1.Value = joints[1].angle = angles[0];
        //    joint2.Value = joints[2].angle = angles[1];
        //    joint3.Value = joints[3].angle = angles[2];
        //    joint4.Value = joints[4].angle = angles[3];
        //    joint5.Value = joints[5].angle = angles[4];

        //    if (timer1.Enabled)
        //    {
        //        button.Content = "Go to position";
        //        isAnimating = false;
        //        timer1.Stop();
        //        movements = 0;
        //    }
        //    else
        //    {
        //        geom.Transform = new TranslateTransform3D(reachingPoint);
        //        movements = 5000;
        //        button.Content = "STOP";
        //        isAnimating = true;
        //        timer1.Start();
        //    }
        //}

        public void timer1_Tick(object sender, EventArgs e)
        {
            execute_fk();
            
        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            joint1.Value = angles_global[0];
            joint2.Value = angles_global[1];
            joint3.Value = angles_global[2];
            joint4.Value = angles_global[3];
            joint5.Value = angles_global[4];

        }
        public double[] InverseKinematics(Vector3D target, double[] angles)
        {
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
            {
                movements = 0;
                return angles;
            }

            double[] oldAngles = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            angles.CopyTo(oldAngles, 0);
            for (int i = 0; i <= 4; i++)
            {
                // Gradient descent
                // Update : Solution -= LearningRate * Gradient
                double gradient = PartialGradient(target, angles, i);
                angles[i] -= LearningRate * gradient;

                // Clamp
                angles[i] = Clamp(angles[i], joints[i].angleMin, joints[i].angleMax);

                // Early termination
                if (DistanceFromTarget(target, angles) < DistanceThreshold || checkAngles(oldAngles, angles))
                {
                    movements = 0;
                    return angles;
                }
            }

            return angles;
        }

        public bool checkAngles(double[] oldAngles, double[] angles)
        {
            for(int i = 0; i <= 4; i++)
            {
                if (oldAngles[i] != angles[i])
                    return false;
            }

            return true;
        }

        public double PartialGradient(Vector3D target, double[] angles, int i)
        {
            // Saves the angle,
            // it will be restored later
            double angle = angles[i];

            // Gradient : [F(x+SamplingDistance) - F(x)] / h
            double f_x = DistanceFromTarget(target, angles);

            angles[i] += SamplingDistance;
            double f_x_plus_d = DistanceFromTarget(target, angles);

            double gradient = (f_x_plus_d - f_x) / SamplingDistance;

            // Restores
            angles[i] = angle;

            return gradient;
        }


        public double DistanceFromTarget(Vector3D target, double[] angles)
        {
            Vector3D point = ForwardKinematics (angles);      
            return Math.Sqrt(Math.Pow((point.X - target.X), 2.0) + Math.Pow((point.Y - target.Y), 2.0) + Math.Pow((point.Z - target.Z), 2.0));
        }
        

        public Vector3D ForwardKinematics(double [] angles)
        {

            /* Variables */
            const int NUM_AFTER_COMMA = 5;
            uint t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0;
            int[] value_positon = new int[16];
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            double t1_dh, t2_dh, t3_dh, t4_dh, x, y, z;

            //The base only has rotation and is always at the origin, so the only transform in the transformGroup is the rotation R
            F1 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, -276.5);
            //R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[0].rotAxisX, joints[0].rotAxisY, joints[0].rotAxisZ), angles[0]), new Point3D(joints[0].rotPointX, joints[0].rotPointY, joints[0].rotPointZ));
            F1.Children.Add(T);
            //F1.Children.Add(R);

            //This moves the first joint attached to the base, it may translate and rotate. Since the joint are already in the right position (the .stl model also store the joints position
            //in the virtual world when they were first created, so if you load all the .stl models of the joint they will be automatically positioned in the right locations)
            //so in all of these cases the first translation is always 0, I just left it for future purposes if something need to be moved
            //After that, the joint needs to rotate of a certain amount (given by the value in the slider), and the rotation must be executed on a specific point
            //After some testing it looks like the point 175, -200, 500 is the sweet spot to achieve the rotation intended for the joint
            //finally we also need to apply the transformation applied to the base 
            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[1].rotAxisX, joints[1].rotAxisY, joints[1].rotAxisZ), angles[0]), new Point3D(joints[1].rotPointX, joints[1].rotPointY, joints[1].rotPointZ));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);

            //The second joint is attached to the first one. As before I found the sweet spot after testing, and looks like is rotating just fine. No pre-translation as before
            //and again the previous transformation needs to be applied
            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[2].rotAxisX, - joints[2].rotAxisY, joints[2].rotAxisZ), (angles[1] + 90.0)), new Point3D(joints[2].rotPointX,  - joints[2].rotPointY, joints[2].rotPointZ));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);

            //as before
            F4 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0); //1500, 650, 1650
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[3].rotAxisX,  - joints[3].rotAxisY, joints[3].rotAxisZ), (angles[2] - 90.0)), new Point3D(joints[3].rotPointX,  - joints[3].rotPointY, joints[3].rotPointZ));
            F4.Children.Add(T);
            F4.Children.Add(R);
            F4.Children.Add(F3);

            //as before
            F5 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[4].rotAxisX, - joints[4].rotAxisY, joints[4].rotAxisZ), angles[3] - 90.0), new Point3D(joints[4].rotPointX,  - joints[4].rotPointY, joints[4].rotPointZ));
            F5.Children.Add(T);
            F5.Children.Add(R);
            F5.Children.Add(F4);

            //NB: I was having a nightmare trying to understand why it was always rotating in a weird way... SO I realized that the order in which
            //you add the Children is actually VERY IMPORTANT in fact before I was applyting F and then T and R, but the previous transformation
            //Should always be applied as last (FORWARD Kinematics)
            F6 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[5].rotAxisX, joints[5].rotAxisY, joints[5].rotAxisZ), angles[4]), new Point3D(joints[5].rotPointX, joints[5].rotPointY, joints[5].rotPointZ));
            F6.Children.Add(T);
            F6.Children.Add(R);
            F6.Children.Add(F5);


            joints[0].model.Transform = F1; //First joint
            joints[1].model.Transform = F2; //Second joint (the "biceps")
            joints[2].model.Transform = F3; //third joint (the "knee" or "elbow")
            joints[3].model.Transform = F4; //the "forearm"
            joints[4].model.Transform = F5; //the tool plate
            joints[5].model.Transform = F6; //the tool

            // Convert the angle from degree to radian and define actual initial position
            t1_dh = angles[0] / 180 * Math.PI;
            t2_dh = (angles[1] + 90.0) / 180 * Math.PI;
            t3_dh = (angles[2] - 90.0) / 180 * Math.PI;
            t4_dh = (angles[3] - 90.0) / 180 * Math.PI;

            // Caculate Foward Kinematic
            x = Math.Cos(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
            y = Math.Sin(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
            z = Constants.l1 + Constants.l2 * Math.Sin(t2_dh) + Constants.l3 * Math.Sin(t2_dh + t3_dh) + Constants.l5 * Math.Sin(t2_dh + t3_dh + t4_dh);

            Tx.Content = x;
            Ty.Content = y;
            Tz.Content = z;

#if IRB6700

#else
            joints[7].model.Transform = F1; //Cables

            joints[8].model.Transform = F2; //Cables

            joints[6].model.Transform = F3; //The ABB writing
            joints[9].model.Transform = F3; //Cables
#endif

            return new Vector3D(joints[5].model.Bounds.Location.X, joints[5].model.Bounds.Location.Y, joints[5].model.Bounds.Location.Z);
        }

        public static (double, double, double, double, double) convert_position_angle(double x, double y, double z)
        {
            double t1, t2, t3, t4, t5, s2, c2, s3, c3, m, n;
            double roll, pitch;
            roll = 0.0;
            pitch = -Math.PI / 2;
            t1 = Math.Atan2(y, x);
            t5 = roll - t1;
            m = Math.Sqrt(x * x + y * y);
            n = z - Constants.l1 + Constants.l5;
            c3 = (m * m + n * n - Constants.l2 * Constants.l2 - Constants.l3 * Constants.l3) / (2 * Constants.l2 * Constants.l3);
            /* s3 has 2 value --> take the value of -sin */
            s3 = Math.Sqrt(1 - c3 * c3);
            t3 = Math.Atan2(s3, c3);
            if (t3 >= -Math.PI / 6 && t3 <= (4 * Math.PI) / 9)
            {
                /* Do nothing*/
            }
            else
            {
                s3 = -1 * Math.Sqrt(1 - c3 * c3);
                t3 = Math.Atan2(s3, c3);
            }
            /* Angle 3 */
            c2 = m * (Constants.l3 * c3 + Constants.l2) + n * (Constants.l3 * s3);
            s2 = n * (Constants.l3 * c3 + Constants.l2) - m * (Constants.l3 * s3);
            /* Angle 2 */
            t2 = Math.Atan2(s2, c2);
            /* Angle 4 */
            t4 = pitch - t2 - t3;
            t1 = t1 / Math.PI * 180.0;
            t2 = t2 / Math.PI * 180.0;
            t3 = t3 / Math.PI * 180.0;
            t4 = t4 / Math.PI * 180.0;
            t5 = t5 / Math.PI * 180.0;
            return (t1, t2, t3, t4, t5);
        }
        private void ConnectPLC(object sender, RoutedEventArgs e)
        {
            /* Start timer1 */
            timer1.Start();
            timer2.Start();



            /* Disable slider */
            joint1.IsEnabled = false;
            joint2.IsEnabled = false;
            joint3.IsEnabled = false;
            joint4.IsEnabled = false;
            joint5.IsEnabled = false;

            /* Disable test position button */
            testpos_bttn = false;
            /* Change the color of the button when clicked */
            ChangeColorObjectBackground(TestPos_bttn, Constants.OBJECT_MODIFIED);
            ChangeColorObjectForeground(TestPos_bttn, Constants.OBJECT_MODIFIED1);
            ChangeColorObjectBorderBrush(TestPos_bttn, Constants.OBJECT_MODIFIED);

            /* Declare the variable(s) */
            int ret;
            /* A logical station number set in Communication Setup Utility - Datasheet - Page 61 */
            plc.ActLogicalStationNumber = 1;
            /* Open the connection between PLC and C# - Datasheet - Page 381 */
            ret = plc.Open();
            /* Return value
               Normal termination : 0 is returned.
               Abnormal termination: Any value other than 0 is returned
            */
            if (ret == 0 && cn_bttn == true)
            {
                //Connect_button.IsEnabled = false;
                //Disconnect_button.IsEnabled = true;
                cn_bttn=false;
                ds_bttn=true;
                /* Change the color of the button when clicked */
                ChangeColorObjectBackground(Connect_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectBackground(Disconnect_button, Constants.OBJECT_MODIFIED1);
                ChangeColorObjectForeground(Connect_button, Constants.OBJECT_MODIFIED1);
                ChangeColorObjectForeground(Disconnect_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectBorderBrush(Disconnect_button, Constants.OBJECT_MODIFIED);

                /* 
                    Print the log command
                    MethosBase.GetCurrentMethod returns the action user did.
                */
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Connect PLC successfully");
            }
            else if (ds_bttn == true)
            {
                PrintLog("Infor", MethodBase.GetCurrentMethod().Name, "PLC was connected");
            }
            else
            {
                PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Connect PLC unsuccessfully");
            }
            /* Read the servo mode */
            int servo_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            /* Read status of Brake and AC Servo */
            ret = PLCReadbit(Constants.R_SERVO_ON, out servo_status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            if (servo_status == 0) /* Servo is currently off */
            {
                Servo_button.Content = "Servo: off";
                ChangeColorObjectBackground(Servo_button, Constants.OBJECT_MODIFIED1);
                ChangeColorObjectForeground(Servo_button, Constants.OBJECT_MODIFIED);
                PrintLog("SERVO:", servo_status.ToString(), "OFF");
            }
            else
            {
                Servo_button.Content = "Servo: on";
                ChangeColorObjectBackground(Servo_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectForeground(Servo_button, Constants.OBJECT_MODIFIED1);
                PrintLog("SERVO:", servo_status.ToString(), "ON");
            }
        }

        private void DisconnectPLC(object sender, RoutedEventArgs e)
        {
            /* Stop timer1 */
            timer1.Stop();
            timer2.Stop();

            /* Enable slider */
            joint1.IsEnabled = true;
            joint2.IsEnabled = true;
            joint3.IsEnabled = true;
            joint4.IsEnabled = true;
            joint5.IsEnabled = true;

            /* Disable test position button */
            testpos_bttn = true;
            /* Change the color of the button when clicked */
            ChangeColorObjectBackground(TestPos_bttn, Constants.OBJECT_MODIFIED1);
            ChangeColorObjectForeground(TestPos_bttn, Constants.OBJECT_MODIFIED);
            ChangeColorObjectBorderBrush(TestPos_bttn, Constants.OBJECT_MODIFIED);
            /* Declare the variable(s) */
            int ret;
            /* Close the connection between PLC and C# - Datasheet - Page 383 */
            ret = plc.Close();
            /* Change the color of the button when clicked */
            //Disconnect_button.Enabled = false;
            //Connect_button.Enabled = true;
            /* 
                Print the log command
                MethosBase.GetCurrentMethod returns the action user did.
            */
            if (ret == 0 && ds_bttn == true)
            {
                //Connect_button.IsEnabled = false;
                //Disconnect_button.IsEnabled = true;
                cn_bttn = true;
                ds_bttn = false;
                /* Change the color of the button when clicked */
                ChangeColorObjectBackground(Connect_button, Constants.OBJECT_MODIFIED1);
                ChangeColorObjectBackground(Disconnect_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectForeground(Connect_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectForeground(Disconnect_button, Constants.OBJECT_MODIFIED1);

                /* 
                    Print the log command
                    MethosBase.GetCurrentMethod returns the action user did.
                */
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Disonnect PLC successfully");
            }
            else if (ds_bttn == false)
            {
                PrintLog("Infor", MethodBase.GetCurrentMethod().Name, "PLC was disconnected");
            }
            else
            {
                PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Disconnect PLC unsuccessfully");
            }

        }

        private void Servo_button_click(object sender, RoutedEventArgs e)
        {
            int ret, servo_status;
            string getName = MethodBase.GetCurrentMethod().Name;

            /* Read status of Brake and AC Servo */
            ret = PLCReadbit(Constants.R_SERVO_ON, out servo_status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            if (servo_status == 0) // Servo is currently OFF
            {
                /* Reverse bit: OnServo_status == 1 */
                Servo_button.Content = "Servo: on";

                /* Change the color of the button when clicked */
                ChangeColorObjectBackground(Servo_button, Constants.OBJECT_MODIFIED);
                ChangeColorObjectForeground(Servo_button, Constants.OBJECT_MODIFIED1);

                PrintLog("SERVO:", servo_status.ToString(), "ON");
                ret = PLCWritebit(Constants.R_SERVO_ON, (~servo_status) & 0x01);
                if (ret != 0)
                {
                    PrintLog("Error", getName, "Write PLC Fail");
                    return;
                }
            }
            else
            {
                /* Reverse bit: OnServo_status == 1 */
                Servo_button.Content = "Servo: off";

                /* Change the color of the button when clicked */
                ChangeColorObjectBackground(Servo_button, Constants.OBJECT_MODIFIED1);
                ChangeColorObjectForeground(Servo_button, Constants.OBJECT_MODIFIED);

                PrintLog("SERVO:", servo_status.ToString(), "OFF");
                ret = PLCWritebit(Constants.R_SERVO_ON, (~servo_status) & 0x01);
                if (ret != 0)
                {
                    PrintLog("Error", getName, "Write PLC Fail");
                    return;
                }
            }
        }

        private void SetHome_button_Click(object sender, RoutedEventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_SETHOME);
        }

        private void ResetError_button_Click(object sender, RoutedEventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_ERR_RESET);
        }

        private void GoHome_button_Click(object sender, RoutedEventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_GOHOME);
        }

        private void Jog_set_speed_Click(object sender, RoutedEventArgs e)
        {
            int velocity = 0;
            try
            {
                velocity = Convert.ToInt32(jog_speed_tb.Text) * 1000;
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(640 + 2 * ind, velocity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Forward_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);
            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGFORWARD1, 1);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;
                    
                case 2:
                    PLCWritebit(Constants.R_JOGGINGFORWARD2, 1);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGFORWARD3, 1);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGFORWARD4, 1);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGFORWARD5, 1);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Forward_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            PLCWritebit(Constants.R_JOGGINGFORWARD1, 0);
            ChangeColorObjectBackground(Forward_button, Constants.OBJECT_WHITE);
            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGFORWARD1, 0);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGFORWARD2, 0);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGFORWARD3, 0);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGFORWARD4, 0);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGFORWARD5, 0);
                    ChangeColorObjectBackground(Forward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Forward_button, Constants.OBJECT_MODIFIED);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Backward_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGINVERSE1, 1);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGINVERSE2, 1);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGINVERSE3, 1);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGINVERSE4, 1);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGINVERSE5, 1);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Backward_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGINVERSE1, 0);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGINVERSE2, 0);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGINVERSE3, 0);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGINVERSE4, 0);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGINVERSE5, 0);
                    ChangeColorObjectBackground(Backward_button, Constants.OBJECT_MODIFIED1);
                    ChangeColorObjectForeground(Backward_button, Constants.OBJECT_MODIFIED);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void set_const_speed_btn_Click(object sender, RoutedEventArgs e)
        {
            int velocity;
            try
            {
                velocity = Convert.ToInt32(spd_tb.Text) * 1000;
                write_d_mem_32_bit(1008, velocity);
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Set Velocity successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tsm_moveJ_btn_Click(object sender, RoutedEventArgs e)
        {
            move = 1; /* moveJ */
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            try
            {
                x = double.Parse(TbX.Text);
                y = double.Parse(TbY.Text);
                z = double.Parse(TbZ.Text);

                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;

                int[] value_angle = new int[10];
                /* Run */
                temp_value[0] = (int)(Convert.ToDouble(t1) * 100000 + 18000000);
                temp_value[1] = (int)(Convert.ToDouble(t2) * 100000 + 18000000);
                temp_value[2] = (int)(Convert.ToDouble(t3) * 100000 + 18000000);
                temp_value[3] = (int)(Convert.ToDouble(t4) * 100000 + 18000000);
                temp_value[4] = (int)(Convert.ToDouble(t5) * 100000 + 18000000);
                /* Write the angle */
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(1010 + 2 * ind, temp_value[ind]);
                }

            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }
        private void TestPos_Click(object sender, RoutedEventArgs e)
        {
            if(testpos_bttn == true)
            {
                double x, y, z;
                double t1, t2, t3, t4, t5;
                int ret;
                int[] temp_value = new int[5];
                try
                {
                    x = double.Parse(TbX.Text);
                    y = double.Parse(TbY.Text);
                    z = double.Parse(TbZ.Text);

                    (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                    ret = Check_angle(t1, t2, t3, t4, t5);
                    if (ret != 0)
                    {
                        double theta = 0.0;
                        if (ret == 1) theta = t1;
                        else if (ret == 2) theta = t2;
                        else if (ret == 3) theta = t3;
                        else if (ret == 4) theta = t4;
                        else if (ret == 5) theta = t5;
                        PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                        return;
                    }
                    t2 -= 90.0;
                    t3 += 90.0;
                    t4 += 90.0;
                    joint1.Value = t1;
                    joint2.Value = t2;
                    joint3.Value = t3;
                    joint4.Value = t4;
                    joint5.Value = t5;

                }
                catch (Exception er)
                {
                    PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
                }
            }
        }

        private void Tsm_moveL_btn_Click(object sender, RoutedEventArgs e)
        {
            move = 2;
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];

            /* Assign corrdination for each array */
            curr_pos[0] = Convert.ToDouble(Tx.Content);
            curr_pos[1] = Convert.ToDouble(Ty.Content);
            curr_pos[2] = Convert.ToDouble(Tz.Content);

            targ_pos[0] = Convert.ToDouble(TbX.Text);
            targ_pos[1] = Convert.ToDouble(TbY.Text);
            targ_pos[2] = Convert.ToDouble(TbZ.Text);

            /* Referred vector */
            for (int i = 0; i < 3; i++)
            {
                vect_u[i] = targ_pos[i] - curr_pos[i];
                //PrintLog("vect", "value", Convert.ToString(vect_u[i]));
            }

            /* Linear Equation */
            for (int t = 0; t < 10; t++)
            {
                x = curr_pos[0] + (vect_u[0] / 10) * (t + 1); /* 500 is the actual position of robot following the x axis */
                y = curr_pos[1] + (vect_u[1] / 10) * (t + 1); /* 0 is the actual position of robot following the y axis */
                z = curr_pos[2] + (vect_u[2] / 10) * (t + 1); /* 900 is the actual position of robot following the y axis */
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Assign value */
                angle_array[t, 0] = (int)(t1 * 100000 + 18000000);
                angle_array[t, 1] = (int)(t2 * 100000 + 18000000);
                angle_array[t, 2] = (int)(t3 * 100000 + 18000000);
                angle_array[t, 3] = (int)(t4 * 100000 + 18000000);
                angle_array[t, 4] = (int)(t5 * 100000 + 18000000);
            }
            for (int j = 0; j < 10; j++)
            {
                value_angle[8 * j] = Write_Theta(angle_array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(angle_array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(angle_array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(angle_array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(angle_array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(angle_array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(angle_array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(angle_array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));

            }
            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);
            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
        }

        private void Tsm_moveC_btn_Click(object sender, RoutedEventArgs e)
        {
            move = 3;
            double x1, y1, x2, y2, x_cur, y_cur, z_cur;
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];
            x_cur = Convert.ToDouble(Tx.Content);
            y_cur = Convert.ToDouble(Ty.Content);
            z_cur = Convert.ToDouble(Tz.Content);

            x1 = Convert.ToDouble(TbX1.Text);
            y1 = Convert.ToDouble(TbY1.Text);

            x2 = Convert.ToDouble(TbX2.Text);
            y2 = Convert.ToDouble(TbY2.Text);
            // Tạo các điểm (x, y)
            Point2 point1 = new Point2(x_cur, y_cur);
            Point2 point2 = new Point2(x1, y1);
            Point2 point3 = new Point2(x2, y2);

            // Tính toán quỹ đạo đường tròn
            Circle circle = CalculateCircle(point1, point2, point3);

            // In kết quả
            Console.WriteLine($"Tâm đường tròn: ({circle.Center.X}, {circle.Center.Y})");
            Console.WriteLine($"Bán kính đường tròn: {circle.Radius}");

            double R = circle.Radius;
            double a = circle.Center.X;
            double b = circle.Center.Y;
            /* Linear Equation */
            for (int t = 0; t < 9; t++)
            {
                x = R * Math.Sin(2 * Math.PI * t / 9) + a;
                y = R * Math.Cos(2 * Math.PI * t / 9) + b;
                z = z_cur;
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Assign value */
                angle_array[t, 0] = (int)(t1 * 100000 + 18000000);
                angle_array[t, 1] = (int)(t2 * 100000 + 18000000);
                angle_array[t, 2] = (int)(t3 * 100000 + 18000000);
                angle_array[t, 3] = (int)(t4 * 100000 + 18000000);
                angle_array[t, 4] = (int)(t5 * 100000 + 18000000);
            }
            for (int j = 0; j < 9; j++)
            {
                value_angle[8 * j] = Write_Theta(angle_array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(angle_array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(angle_array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(angle_array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(angle_array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(angle_array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(angle_array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(angle_array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));
            }
            value_angle[8 * 9] = Write_Theta(angle_array[0, 0])[0];
            value_angle[8 * 9 + 1] = Write_Theta(angle_array[0, 0])[1];

            value_angle[8 * 9 + 2] = Write_Theta(angle_array[0, 1])[0];
            value_angle[8 * 9 + 3] = Write_Theta(angle_array[0, 1])[1];

            value_angle[8 * 9 + 4] = Write_Theta(angle_array[0, 2])[0];
            value_angle[8 * 9 + 5] = Write_Theta(angle_array[0, 2])[1];

            value_angle[8 * 9 + 6] = Write_Theta(angle_array[0, 3])[0];
            value_angle[8 * 9 + 7] = Write_Theta(angle_array[0, 3])[1];

            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);
            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
        }
        private void run_bttn_Click(object sender, RoutedEventArgs e)
        {
            if (move == 1)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(528);
            }
            else if (move == 2)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(530);
            }
            else if (move == 3)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(530);
            }
            if(cn_bttn == false)
            {
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Run Successfully");
            }
            else
            {
                PrintLog("Error", MethodBase.GetCurrentMethod().Name, "PLC is not connected");
            }
            move = 0;
        }

        private void helixViewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(viewPort3d);

            // Find the nearest visual in the 3D scene
            HitTestResult result = VisualTreeHelper.HitTest(viewPort3d, mousePosition);
            RayMeshGeometry3DHitTestResult meshResult = result as RayMeshGeometry3DHitTestResult;

            if (meshResult != null)
            {
                Point3D clickedPoint = meshResult.PointHit;

                // Update the label content on the UI thread
                Dispatcher.Invoke(() =>
                {
                    coordinatesLabel.Content = $"Coordinates: ({clickedPoint.X}, {clickedPoint.Y}, {clickedPoint.Z})";
                });
            }
        }

        private void Press_button(string name, string adr)
        {
            int ret;
            ret = PLCWritebit(adr, 1);
            if (ret != 0)
            {
                PrintLog("Error", name, string.Format("Set {0} = 1 Failed", adr));
                return;
            }
            Thread.Sleep(1);
            ret = PLCWritebit(adr, 0);
            if (ret != 0)
            {
                PrintLog("Error", name, string.Format("Set {0} = 0 Failed", adr));
                return;
            }
            PrintLog("Info", name, string.Format("Raise {0} Successfully", adr));
        }

        public int PLCReadbit(string adr, out int receive)
        {
            return plc.GetDevice(adr, out receive);
        }
        public int PLCWritebit(string adr, int value)
        {
            return plc.SetDevice(adr, value);
        }
        public void PrintLog(string level, string namefunction, string msg)
        {
            DateTime time = DateTime.Now;
            ErrorLog.AppendText(time.ToString("h:mm:ss") + " - " + level + " - " + namefunction + ": " + msg);
            ErrorLog.AppendText(Environment.NewLine);
        }
        public void ChangeColorObjectBackground(object objectin, Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.Background = new SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.Background = new SolidColorBrush(color_object);
                return;
            }
        }
        public void ChangeColorObjectForeground(object objectin, Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.Foreground = new SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.Foreground = new SolidColorBrush(color_object);
                return;
            }
        }

        public void ChangeColorObjectBorderBrush(object objectin, Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.BorderBrush = new SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.BorderBrush = new SolidColorBrush(color_object);
                return;
            }
        }
        public int[] Write_Theta(int value_angle)
        {
            int[] value_angle_arr = new int[2];
            value_angle_arr[0] = value_angle & 0xFFFF; //byte high for register
            value_angle_arr[1] = (value_angle >> 16) & 0xFFFF; // byte low for register
            return value_angle_arr;
        }

        public void write_d_mem_32_bit(int device, int data)
        {
            string device_str = "";
            device_str = "D" + Convert.ToString(device);
            int[] temp = new int[2];
            temp[0] = data & 0xFFFF; //byte high for register
            temp[1] = (data >> 16) & 0xFFFF; // byte low for register
            /* Write the angle */
            plc.WriteDeviceBlock(device_str, 2, ref temp[0]);
        }
        public int Check_angle(double t1, double t2, double t3, double t4, double t5)
        {
            if ((t1 > Constants.T1_LU) || (t1 < Constants.T1_LD) || double.IsNaN(t1))
            {
                return 1;
            }
            if ((t2 > Constants.T2_LU) || (t2 < Constants.T2_LD) || double.IsNaN(t2))
            {
                return 2;
            }
            if ((t3 > Constants.T3_LU) || (t3 < Constants.T3_LD) || double.IsNaN(t3))
            {
                return 3;
            }
            if ((t4 > Constants.T4_LU) || (t4 < Constants.T4_LD) || double.IsNaN(t4))
            {
                return 4;
            }
            if ((t5 > Constants.T5_LU) || (t5 < Constants.T5_LD) || double.IsNaN(t5))
            {
                return 5;
            }
            return 0;
        }
        // Convert value read from PLC to int value
        public uint Read_Position(uint value_positon1, uint value_positon2)
        {
            return (value_positon2 << 16 | value_positon1) - 18000000;
        }

        private async void TCP_Connect_button_Click(object sender, RoutedEventArgs e)
        {
            // Call the StartClient method to initiate the connection and communication with the server
            StartClient();

        }
        private void StartClient()
        {
            // Create a socket object
            using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // Connect to the server
                string host = addr_tb.Text;
                int port = Convert.ToInt16(port_tb.Text);

                try
                {
                    clientSocket.Connect(host, port);
                    //MessageBox.Show($"Connected to {host}:{port}");
                    PrintLog("Infor", "Connected to", $"{host}:{port}");

                    // Send the command to the server
                    string commandToSend = data_tb.Text + "\r\n";
                    byte[] commandBytes = Encoding.ASCII.GetBytes(commandToSend);
                    Console.WriteLine(Encoding.ASCII.GetString(commandBytes));
                    clientSocket.Send(commandBytes);

                    // Receive the response from the server
                    byte[] buffer = new byte[1024];
                    int bytesRead = clientSocket.Receive(buffer);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                }
                catch (Exception ex)
                {
                    PrintLog("Error", "Unable to connect to", $"{host}:{port}");
                }
            }
        }

        private void TCP_sendata_button_Click(object sender, RoutedEventArgs e)
        {
            // Create a socket object
            using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                // Connect to the server
                string host = addr_tb.Text;
                int port = Convert.ToInt16(port_tb.Text);

                try
                {
                    clientSocket.Connect(host, port);

                    // Send the command to the server
                    string commandToSend = data_tb.Text + "\r\n";
                    byte[] commandBytes = Encoding.ASCII.GetBytes(commandToSend);
                    Console.WriteLine(Encoding.ASCII.GetString(commandBytes));
                    clientSocket.Send(commandBytes);

                    // Receive the response from the server
                    byte[] buffer = new byte[307200];
                    int bytesRead = clientSocket.Receive(buffer);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    PrintLog("Infor", "Data received", response);
                    // Sentences to remove
                    string[] sentencesToRemove = { "1003000307200", "1003*", "1003?" };

                    // Loop through each sentence and replace it with an empty string
                    foreach (string sentence in sentencesToRemove)
                    {
                        response = response.Replace(sentence, "");
                    }
                    
                    // Specify the folder path where you want to save the file
                    string folderPath = @"C:\Users\daveb\Desktop\raw_data\";

                    // Construct the full file path using Path.Combine
                    string filePath = System.IO.Path.Combine(folderPath, "response_bytes.txt");

                    // Save the received data to the byte file
                    using (StreamWriter file = new StreamWriter(filePath, false)) // Use false to overwrite the file
                    {
                        foreach (byte byteValue in buffer)
                        {
                            file.WriteLine(byteValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PrintLog("Error", "Unable to connect to", $"{host}:{port}");
                }
            }
        }

        private void Check_length_button_Click(object sender, RoutedEventArgs e)
        {
            // Replace "your_file_path.txt" with the actual path to your text file
            string filePath = "C:\\Users\\daveb\\Desktop\\raw_data\\response_bytes.txt";

            // Read all text from the file using UTF-8 encoding
            string[] fileline = File.ReadAllLines(filePath);

            // Check if the file contains enough characters for a 640x480 array
            if (fileline.Length != 307200)
            {
                PrintLog("Error", "", "File does not contain enough characters for a 640x480 array.");
                return;
            }

            // Create a 2D array with dimensions 640x480
            array2D = new byte[640, 480];

            // Populate the 2D array
            for (int y = 0; y < 480; y++)
            {
                // Convert each line into an array of bytes
                byte[] lineBytes = new byte[640];
                for (int x = 0; x < 640; x++)
                {
                    lineBytes[x] = Convert.ToByte(fileline[y * 640 + x]);
                }

                for (int x = 0; x < 640; x++)
                {
                    // Assign the byte to the 2D array
                    array2D[x, y] = lineBytes[x];
                }
            }

            // Now you have a 2D array (640x480) containing the characters from the text file
            PrintLog("Infor", "", "2D Array created successfully.");
            //Console.WriteLine("2D Array created successfully.");
        }

        private void Camera_button_Click(object sender, RoutedEventArgs e)
        {
            // Display the image
            DisplayImage();
        }

        private void DisplayImage()
        {
            // Create a BitmapSource from the 2D byte array
            BitmapSource bitmapSource = BitmapSource.Create(
                640, 480,
                96, 96,
                PixelFormats.Gray8,
                null,
                array2D,
                640); // Stride = width of the image in bytes

            // Create an Image control
            Image image = new Image();
            image.Source = bitmapSource;

            // Add the Image control to your WPF layout (assuming you have a Grid named "mainGrid" in your XAML)
            mainGrid.Children.Add(image);
        }

        public void turn_on_1_pulse_relay(int device)
        {
            string device_str = "M" + Convert.ToString(device);
            int status, ret;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            /* Turn on relay */
            /* Read status */
            ret = PLCReadbit(device_str, out status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            /* Reverse bit: status == 1 */
            ret = PLCWritebit(device_str, (~status) & 0x01);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Write PLC Fail");
                return;
            }
            PLCReadbit(device_str, out readbit);
            PrintLog("Info", device_str, Convert.ToString(readbit));

            /* Reverse it into the initial state */
            ret = PLCWritebit(device_str, (~(~status)) & 0x01);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Write PLC Fail");
                return;
            }
            PLCReadbit(device_str, out readbit);
            PrintLog("Info", device_str, Convert.ToString(readbit));
        }

        class Point2
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point2(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        class Circle
        {
            public Point2 Center { get; set; }
            public double Radius { get; set; }

            public Circle(Point2 center, double radius)
            {
                Center = center;
                Radius = radius;
            }
        }
        static Circle CalculateCircle(Point2 point1, Point2 point2, Point2 point3)
        {
            double x1 = point1.X;
            double y1 = point1.Y;
            double x2 = point2.X;
            double y2 = point2.Y;
            double x3 = point3.X;
            double y3 = point3.Y;

            double x12 = x1 - x2;
            double x13 = x1 - x3;

            double y12 = y1 - y2;
            double y13 = y1 - y3;

            double y31 = y3 - y1;
            double y21 = y2 - y1;

            double x31 = x3 - x1;
            double x21 = x2 - x1;

            double sx13 = Math.Pow(x1, 2) - Math.Pow(x3, 2);
            double sy13 = Math.Pow(y1, 2) - Math.Pow(y3, 2);

            double sx21 = Math.Pow(x2, 2) - Math.Pow(x1, 2);
            double sy21 = Math.Pow(y2, 2) - Math.Pow(y1, 2);

            double f = ((sx13) * (x12) + (sy13) * (x12) + (sx21) * (x13) + (sy21) * (x13)) / (2 * ((y31) * (x12) - (y21) * (x13)));
            double g = ((sx13) * (y12) + (sy13) * (y12) + (sx21) * (y13) + (sy21) * (y13)) / (2 * ((x31) * (y12) - (x21) * (y13)));

            double c = -Math.Pow(x1, 2) - Math.Pow(y1, 2) - 2 * g * x1 - 2 * f * y1;

            double h = -g;
            double k = -f;
            double sqr_of_r = h * h + k * k - c;

            double r = Math.Sqrt(sqr_of_r);

            return new Circle(new Point2(h, k), r);
        }


    }

}
