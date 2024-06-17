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

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Net.Sockets;
using System.IO.Ports;
using OxyPlot;
using OxyPlot.Series;

using System.Windows.Threading;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using RobotArmHelix.Properties;
using System.Windows.Markup;
using OxyPlot.Axes;
using System.ComponentModel;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Direct2D1;
using CsvHelper;
using CsvHelper.Configuration;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Globalization;
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

        public int visible_robot = 1;
        public int visible_display = 1;
        public int visible_control = 1;
        public int visible_jogging = 1;
        public int visible_path = 1;
        public int visible_glove = 1;
        public int status_first_time = 0;
        private bool write_csv = false;
                        int value = 0;
        int vel_1_test;
        double[] point1_test = new double[3];
        double[] point2_test = new double[3];

        private double returnX = 500;
        private double returnY = 0;
        private double returnZ = 600;
        // Define the file path
        public string filePath = @"H:/OneDrive - hcmute.edu.vn/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv";
        public string savePath = @"H:/OneDrive - hcmute.edu.vn/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/good.csv";

        public string[] fields;
        public string[] totalLines_csv;

        double t1_test, t2_test, t3_test, t4_test, t5_test;
        double t5_camera = 0.0;

        private int glove_enable = 0;

        private int servo_status_timer = 0;
        private System.Timers.Timer timer;

        private SerialPort uart = new SerialPort();
        string receivedData;
        /* Create the list for later removal */
        private List<ModelVisual3D> sphereVisuals = new List<ModelVisual3D>();

        private Thread subThread1;
        private Thread subThread2;
        private bool Thread1isRunning = true;
        private bool Thread2isRunning = true;

        private float elapsedTimeInSeconds = 0; // Track elapsed time in seconds

        private byte[,] array2D;
        //Declaration for connecting TCP/IP
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        //provides functionality to 3d models
        Model3DGroup RA = new Model3DGroup(); //RoboticArm 3d group
        Model3D geom = null; //Debug sphere to check in which point the joint is rotatin
        Model3D geomtest = null; //Debug sphere to check in which point the joint is rotatin
        public ActUtlType plc = new();
        List<Joint> joints = null;
        int move = 0; /* move = 1 -> MoveJ, move = 2 -> MoveL */

        bool switchingJoint = false;
        bool isAnimating = false;

        string response_client;

        public double joint1_value, joint2_value, joint3_value, joint4_value, joint5_value;
        public double[] axis = {0, 0, 0};

        public double[] angles_global = {0, 0, 0, 0, 0};

        public bool cn_bttn = true;
        public bool ds_bttn = false;
        public bool testpos_bttn = true;

        System.Windows.Media.Color oldColor = Colors.White;
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

        // Tạo một đối tượng StreamWriter để ghi dữ liệu vào tập tin CSV
        static string g_csvFilePath = "H:\\OneDrive - hcmute.edu.vn\\Desktop\\5_DOF_Robot_Arm\\gui\\robot-tool-c#\\RobotArmHelix\\data\\test.csv";

#if IRB6700
        //directroy of all stl files
        private const string MODEL_PATH0 = "K0.stl";
        private const string MODEL_PATH1 = "K1.stl";
        private const string MODEL_PATH2 = "K2.stl";
        private const string MODEL_PATH3 = "K3.stl";
        private const string MODEL_PATH4 = "K4.stl";
        private const string MODEL_PATH5 = "K5.stl";

        private readonly PlotModel _plotModel_position;
        private readonly PlotModel _plotModel_theta;
        private readonly PlotModel _plotModel_omega;
        private readonly PlotModel _plotModel_velocity;
        private readonly PlotModel _plotModel_robot_pos;
        private readonly PlotModel _plotModel_glove_pos;

        private readonly DispatcherTimer _timer;
        private readonly BackgroundWorker _uartWorker;

        public MainWindow()
        {
            InitializeComponent();
            //UART
            string[] ports = SerialPort.GetPortNames();
            com_port_list1.ItemsSource = ports;

            // uart.DataReceived += SerialPort_DataReceived;

            //// Attach the event handler to the MouseDown event
            //viewPort3d.MouseDown += helixViewport3D_MouseDown;
            basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\3D_Models\\";
            List<string> modelsNames = new List<string>();
            modelsNames.Add(MODEL_PATH0);
            modelsNames.Add(MODEL_PATH1);
            modelsNames.Add(MODEL_PATH2);
            modelsNames.Add(MODEL_PATH3);
            modelsNames.Add(MODEL_PATH4);
            modelsNames.Add(MODEL_PATH5);

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

            #region Timer_Declaration

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 100;
            timer1.Tick += new System.EventHandler(timer1_Tick);

            #endregion

            #region Plot_Model

            // Create plot model
            _plotModel_position = new PlotModel { Title = "Robot Oxyz" };
            _plotModel_theta = new PlotModel { Title = "Robot Theta" };
            _plotModel_omega = new PlotModel { Title = "Robot Omega" };
            _plotModel_velocity = new PlotModel { Title = "Robot Velocity" };
            _plotModel_robot_pos = new PlotModel { Title = "Robot Position Oxy" };
            _plotModel_glove_pos = new PlotModel { Title = "Glove Position" };

            // Create line series for each value
            var series_x_pos = new LineSeries { Title = "x_pos" };
            var series_y_pos = new LineSeries { Title = "y_pos" };
            var series_z_pos = new LineSeries { Title = "z_pos" };

            var _scatterSeries_robot_pos = new ScatterSeries
            {
                Title = "Robot Position Oxy",
                MarkerType = MarkerType.Circle, // Loại marker (điểm) để hiển thị
                MarkerSize = 4, // Kích thước của marker
                MarkerFill = OxyColors.Blue // Màu sắc của marker
            };

            // Thêm dữ liệu vào ScatterSeries
            _plotModel_robot_pos.Series.Add(_scatterSeries_robot_pos);

            // Add series to plot model
            _plotModel_position.Series.Add(series_x_pos);
            _plotModel_position.Series.Add(series_y_pos);
            _plotModel_position.Series.Add(series_z_pos);

            // Set plot model to PlotView
            plotView_position.Model = _plotModel_position;
            plotView_robot_pos.Model = _plotModel_robot_pos;

            #endregion

            #region UART_Claration

            // Initialize BackgroundWorker
            _uartWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _uartWorker.DoWork += UartWorker_DoWork;
            
            #endregion

        }

        #region Thread_Timer

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
        private void Thread2Start()
        {
            Thread2isRunning = true;
            subThread2 = new Thread(SubThread2Work);
            subThread2.Start();
        }
        private void SubThread2Work()
        {
            // Simulated work in the sub-thread
            while (Thread2isRunning)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        joint1.Value = angles_global[0];
                        joint2.Value = angles_global[1];
                        joint3.Value = angles_global[2];
                        joint4.Value = angles_global[3];
                        joint5.Value = angles_global[4];
                    });
                }
                catch (TaskCanceledException)
                {
                    // Ignore the exception
                }

                // Sleep for a while to simulate the function taking time
                Thread.Sleep(5000); // Simulate a function execution time of 100 milliseconds
            }
        }

        public void Thread1Start()
        {
            Thread1isRunning = true;
            subThread1 = new Thread(SubThread1Work);
            subThread1.Start();
        }

        private void SubThread1Work()
        {
            // Simulated work in the sub-thread
            while (Thread1isRunning)
            {
                // Execute your function here
                execute_fk();

                // Sleep for a while to simulate the function taking time
                Thread.Sleep(1); // Simulate a function execution time of 100 milliseconds
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            try
            {

                x = returnX;
                y = returnY;
                z = returnZ;

                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5_camera);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5_camera;
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
                temp_value[4] = (int)(Convert.ToDouble(t5_camera) * 100000 + 18000000);
                /* Write the angle */
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(1400 + 2 * ind, temp_value[ind]);
                }
            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }

        #endregion

        private Model3DGroup Initialize_Environment(List<string> modelsNames)
        {
            try
            {
                ModelImporter import = new ModelImporter();
                joints = new List<Joint>();

                foreach(string modelName in modelsNames)
                {
                    var materialGroup = new MaterialGroup();
                    System.Windows.Media.Color mainColor = Colors.White;
                    EmissiveMaterial emissMat = new EmissiveMaterial(new System.Windows.Media.SolidColorBrush(mainColor));
                    DiffuseMaterial diffMat = new DiffuseMaterial(new System.Windows.Media.SolidColorBrush(mainColor));
                    SpecularMaterial specMat = new SpecularMaterial(new System.Windows.Media.SolidColorBrush(mainColor), 200);
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

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
        }

        #region Jaocbi

        public static double[] MultiplyMatrices(double[,] matrix5x3, double[,] matrix3x1)
        {
            int rowsMatrix5x3 = matrix5x3.GetLength(0);
            int colsMatrix5x3 = matrix5x3.GetLength(1);
            int rowsMatrix3x1 = matrix3x1.GetLength(0);
            int colsMatrix3x1 = matrix3x1.GetLength(1);

            if (colsMatrix5x3 != rowsMatrix3x1)
            {
                throw new InvalidOperationException("Matrix dimensions do not match for multiplication.");
            }

            double[] resultMatrix = new double[rowsMatrix5x3];

            for (int i = 0; i < rowsMatrix5x3; i++)
            {
                double sum = 0;
                for (int j = 0; j < colsMatrix5x3; j++)
                {
                    sum += matrix5x3[i, j] * matrix3x1[j, 0];
                }
                resultMatrix[i] = sum;
            }

            return resultMatrix;
        }

        public static double[,] CreateVelocityMatrix(double vx, double vy, double vz)
        {
            double[,] matrix = new double[3, 1]
            {
            { vx },
            { vy },
            { vz }
            };

            return matrix;
        }

        public static double[,] CreateJacobianMatrix(double t1, double t2, double t3, double t4, double t5)
        {
            double[,] jacobianMatrix = new double[5, 3]
            {
                {
                    -Math.Sin(t1) / (Constants.l3 * Math.Cos(t2 + t3) + Constants.l2 * Math.Cos(t2)),
                    Math.Cos(t1) / (Constants.l3 * Math.Cos(t2 + t3) + Constants.l2 * Math.Cos(t2)),
                    0
                },
                {
                    (Math.Cos(t2 + t3) * Math.Cos(t1)) / (Constants.l2 * Math.Sin(t3)),
                    (Math.Cos(t2 + t3) * Math.Sin(t1)) / (Constants.l2 * Math.Sin(t3)),
                    Math.Sin(t2 + t3) / (Constants.l2 * Math.Sin(t3))
                },
                {
                    -(Math.Cos(t1) * (Constants.l3 * Math.Cos(t2 + t3) + Constants.l2 * Math.Cos(t2))) / (Constants.l2 * Constants.l3 * Math.Sin(t3)),
                    -(Math.Sin(t1) * (Constants.l3 * Math.Cos(t2 + t3) + Constants.l2 * Math.Cos(t2))) / (Constants.l2 * Constants.l3 * Math.Sin(t3)),
                    -(Constants.l3 * Math.Sin(t2 + t3) + Constants.l2 * Math.Sin(t2)) / (Constants.l2 * Constants.l3 * Math.Sin(t3))
                },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            return jacobianMatrix;
        }

        #endregion

        #region GUI_display

        private System.Windows.Media.Color changeModelColor(Joint pJoint, System.Windows.Media.Color newColor)
        {
            Model3DGroup models = ((Model3DGroup)pJoint.model);
            return changeModelColor(models.Children[0] as GeometryModel3D, newColor);
        }

        private System.Windows.Media.Color changeModelColor(GeometryModel3D pModel, System.Windows.Media.Color newColor)
        {
            if (pModel == null)
                return oldColor;

            System.Windows.Media.Color previousColor = Colors.Black;

            MaterialGroup mg = (MaterialGroup)pModel.Material;
            if (mg.Children.Count > 0)
            {
                try
                {
                    previousColor = ((EmissiveMaterial)mg.Children[0]).Color;
                    ((EmissiveMaterial)mg.Children[0]).Color = newColor;
                    ((DiffuseMaterial)mg.Children[1]).Color = newColor;
                }
                catch
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
            catch
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
           System.Windows.Point mousePos = e.GetPosition(viewPort3d);
           PointHitTestParameters hitParams = new PointHitTestParameters(mousePos);
           VisualTreeHelper.HitTest(viewPort3d, null, ResultCallback, hitParams);
        }

        private void ViewPort3D_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Perform the hit test on the mouse's position relative to the viewport.
            System.Windows.Media.HitTestResult result = VisualTreeHelper.HitTest(viewPort3d, e.GetPosition(viewPort3d));
            RayMeshGeometry3DHitTestResult mesh_result = result as RayMeshGeometry3DHitTestResult;

            if (oldSelectedModel != null)
                unselectModel();

            if (mesh_result != null)
            {
                selectModel(mesh_result.ModelHit);
            }
        }

        public HitTestResultBehavior ResultCallback(System.Windows.Media.HitTestResult result)
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

        public void PrintLog(string level, string namefunction, string msg)
        {
            DateTime time = DateTime.Now;
            ErrorLog.AppendText(time.ToString("h:mm:ss") + " - " + level + " - " + namefunction + ": " + msg);
            ErrorLog.AppendText(Environment.NewLine);
        }

        public void ChangeColorObjectBackground(object objectin, System.Windows.Media.Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.Background = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.Background = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }
        }

        public void ChangeColorObjectForeground(object objectin, System.Windows.Media.Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.Foreground = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.Foreground = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }
        }

        public void ChangeColorObjectBorderBrush(object objectin, System.Windows.Media.Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.BorderBrush = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.BorderBrush = new System.Windows.Media.SolidColorBrush(color_object);
                return;
            }
        }

        public Vector3D ForwardKinematics(double[] angles)
        {

            /* Variables */
            int[] value_positon = new int[16];
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
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[2].rotAxisX, -joints[2].rotAxisY, joints[2].rotAxisZ), (angles[1] + 90.0)), new Point3D(joints[2].rotPointX, -joints[2].rotPointY, joints[2].rotPointZ));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);

            //as before
            F4 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0); //1500, 650, 1650
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[3].rotAxisX, -joints[3].rotAxisY, joints[3].rotAxisZ), (angles[2] - 90.0)), new Point3D(joints[3].rotPointX, -joints[3].rotPointY, joints[3].rotPointZ));
            F4.Children.Add(T);
            F4.Children.Add(R);
            F4.Children.Add(F3);

            //as before
            F5 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(joints[4].rotAxisX, -joints[4].rotAxisY, joints[4].rotAxisZ), angles[3] - 90.0), new Point3D(joints[4].rotPointX, -joints[4].rotPointY, joints[4].rotPointZ));
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

            /* Draw trajectory */
            List<Point3D> trajectoryPoints = new List<Point3D>();
            // Create a debug sphere at the trajectory point
            MeshBuilder buildertest = new MeshBuilder();
            buildertest.AddSphere(new Point3D(x + 250, y + 250, z + 250), 5, 15, 15); // Adjust the radius as needed

            // Create a GeometryModel3D using the mesh and a blue material
            GeometryModel3D sphereModel = new GeometryModel3D(buildertest.ToMesh(), Materials.Red);

            // Create a ModelVisual3D to hold the GeometryModel3D
            ModelVisual3D visualtest = new ModelVisual3D();
            visualtest.Content = sphereModel;

            // Add the ModelVisual3D to the Viewport3D
            viewPort3d.Children.Add(visualtest);
            // Add the ModelVisual3D to the list for later removal
            sphereVisuals.Add(visualtest);

            return new Vector3D(joints[5].model.Bounds.Location.X, joints[5].model.Bounds.Location.Y, joints[5].model.Bounds.Location.Z);
        }

        private void RemoveSphereVisuals()
        {
            foreach (ModelVisual3D visual in sphereVisuals)
            {
                viewPort3d.Children.Remove(visual);
            }
            sphereVisuals.Clear();
        }

        private void ReachingPoint_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                reachingPoint = new Vector3D(Double.Parse(TbX.Text), Double.Parse(TbY.Text), Double.Parse(TbZ.Text));
            }
            catch
            {

            }
        }

        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isAnimating)
                return;
            if (cn_bttn == false)
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

        #endregion

        #region control
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
        #endregion

        #region PLC_connect
        private void ConnectPLC(object sender, RoutedEventArgs e)
        {
            /* Change state of the menu */
            // After connecting, uncheck the "Disconnect" MenuItem
            DisconnectMenuItem.IsChecked = false;
            ConnectMenuItem.IsChecked = true;
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
                cn_bttn = false;
                ds_bttn = true;
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
            /* Start timer1 and timer2 */
            // timer1.Start();
            // Thread1Start();
            // Thread1Start();
            //Thread2Start();
            //timer1.Start();
        }
        private void DisconnectPLC(object sender, RoutedEventArgs e)
        {
            /* Change state of the menu item */
            // After connecting, uncheck the "Disconnect" MenuItem
            DisconnectMenuItem.IsChecked = true;
            ConnectMenuItem.IsChecked = false;
            /* Stop thread */
            Thread1isRunning = false;
            Thread2isRunning = false;

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
        #endregion

        #region Jogging_control
        private void Jog_set_speed_Click(object sender, RoutedEventArgs e)
        {
            int velocity = 0;
            string getName = MethodBase.GetCurrentMethod().Name;
            try
            {
                velocity = Convert.ToInt32(jog_speed_tb.Text) * 1000;
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(640 + 2 * ind, velocity);
                }
                PrintLog("Infor", getName, "Write velocity to PLC successfully");

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
        #endregion

        #region path_control
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

                if (z >= 500 && z <= 1000)
                {
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
                else
                {
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", "Out of range of Z axis"));
                }
            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }
        private void TestPos_Click(object sender, RoutedEventArgs e)
        {
            if (testpos_bttn == true)
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
        private void MoveJ_Function(double[] targ_pos, int device)
        {
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            int[,] angle_array = new int[10, 5];
            try
            {
                x = targ_pos[0];
                y = targ_pos[1];
                z = targ_pos[2];

                if (z >= 500 && z <= 1000)
                {
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
                        write_d_mem_32_bit(device + 2 * ind, temp_value[ind]);
                    }
                }
                else
                {
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", "Out of range of Z axis"));
                }
            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }
        private void Move_mod_Function(double[,] tar_pos, string device)
        {
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];

            /* Linear Equation */
            for (int t = 0; t < 10; t++)
            {
                x = tar_pos[t,0];
                y = tar_pos[t,1];
                z = tar_pos[t,2];
                if (z >= 500 && z <= 1000)
                {
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
                        //PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
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
                    angle_array[t, 4] = (int)(t5 * 100000);
                }
                else
                {
                    //PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", "Out of range of Z axis"));
                }

            }
            Memory_angle_write(angle_array, value_angle, device, 10);

            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
        }
        private void MoveL_Function(double[] curr_pos, double[] targ_pos, string device)
        {
            double[] vect_u = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];
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
                if (z >= 500 && z <= 1000)
                {
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
                    angle_array[t, 4] = (int)(t5 * 100000);
                }
                else
                {
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", "Out of range of Z axis"));
                }

            }
            Memory_angle_write(angle_array, value_angle, device, 10);

            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
        }
        private void Tsm_moveL_btn_Click(object sender, RoutedEventArgs e)
        {
            move = 2;
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            int[,] angle_array = new int[10, 5];
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];

            /* Assign corrdination for each array */
            curr_pos[0] = Convert.ToDouble(Tx.Content);
            curr_pos[1] = Convert.ToDouble(Ty.Content);
            curr_pos[2] = Convert.ToDouble(Tz.Content);

            targ_pos[0] = Convert.ToDouble(TbX.Text);
            targ_pos[1] = Convert.ToDouble(TbY.Text);
            targ_pos[2] = Convert.ToDouble(TbZ.Text);

            MoveL_Function(curr_pos, targ_pos, "D1010");
        }
        private void run_bttn_Click(object sender, RoutedEventArgs e)
        {
            if (move == 1)
            {
                // turn_on_1_pulse_relay(528);
                Press_button(MethodBase.GetCurrentMethod().Name, "M528");
            }
            else if (move == 2)
            {
                // turn_on_1_pulse_relay(530);
                Press_button(MethodBase.GetCurrentMethod().Name, "M530");
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
        #endregion

        #region PLC_library_functions

        public int PLCReadbit(string adr, out int receive)
        {
            return plc.GetDevice(adr, out receive);
        }
        public int PLCWritebit(string adr, int value)
        {
            return plc.SetDevice(adr, value);
        }

        #endregion

        #region checking_functions
        private void execute_fk()
        {
            int[] value_positon = new int[16];
            uint t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0;
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            if (cn_bttn == true)
            {
                double[] angles = { joints[1].angle, joints[2].angle, joints[3].angle, joints[4].angle, joints[5].angle };
                // Update UI asynchronously using Dispatcher
                Dispatcher.Invoke(() =>
                {
                    /* Update position for robot on GUI */
                    ForwardKinematics(angles);
                    /* Update data for slider on GUI */
                    joint1.Value = angles[0];
                    joint2.Value = angles[1];
                    joint3.Value = angles[2];
                    joint4.Value = angles[3];
                    joint5.Value = angles[4];
                });
            }
            else
            {
                /* Read position of 5 angles */
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
                try
                {
                    // Update UI asynchronously using Dispatcher
                    Dispatcher.Invoke(() =>
                    {
                        /* Update position for robot on GUI */
                        ForwardKinematics(angles_global);
                    });
                }
                catch (TaskCanceledException)
                {
                    // Ignore the exception
                }
            }
        }
        public bool checkAngles(double[] oldAngles, double[] angles)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (oldAngles[i] != angles[i])
                    return false;
            }

            return true;
        }
        public static (double, double, double, double, double) convert_position_angle(double x, double y, double z)
        {
            double t1, t2, t3, t4, t5, s2, c2, s3, c3, m, n;
            double pitch;
            pitch = -Math.PI / 2;
            t1 = Math.Atan2(y, x);
            // t5 = roll - t1;
            t5 = 0.0;
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

        public int[] Write_Theta(int value_angle)
        {
            int[] value_angle_arr = new int[2];
            value_angle_arr[0] = value_angle & 0xFFFF; //byte high for register
            value_angle_arr[1] = (value_angle >> 16) & 0xFFFF; // byte low for register
            return value_angle_arr;
        }
        private void Memory_angle_write(int[,] array, int[] value_angle, string device, int point)
        {
            for (int j = 0; j < point; j++)
            {
                value_angle[8 * j] = Write_Theta(array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));
            }
            plc.WriteDeviceBlock(device, 8 * point, ref value_angle[0]);
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
        public int Check_position(double position_x, double position_y, double position_z)
        {
            if ((position_x > Constants.T1_LU) || (position_x < Constants.T1_LD) || double.IsNaN(position_x))
            {
                return 1;
            }
            if ((position_y > Constants.T2_LU) || (position_y < Constants.T2_LD) || double.IsNaN(position_y))
            {
                return 2;
            }
            if ((position_z > Constants.T3_LU) || (position_z < Constants.T3_LD) || double.IsNaN(position_z))
            {
                return 3;
            }
            return 0;
        }
        // Convert value read from PLC to int value
        public uint Read_Position(uint value_positon1, uint value_positon2)
        {
            return (value_positon2 << 16 | value_positon1) - 18000000;
        }

        #endregion

        #region uart
        private void UartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // UART transmission logic
            while (!worker.CancellationPending)
            {
                if (uart.BytesToRead > 0)
                {
                    // Read available bytes
                    byte[] byteArray = new byte[uart.BytesToRead];
                    uart.Read(byteArray, 0, byteArray.Length);
                    // Convert the byte array to a hexadecimal string
                    if (byteArray.Length >= 19)
                    {
                        byte crc_byte = 0x00;
                        crc_byte = Lc709204fCalculateCrc8Atm(byteArray, 11);
                        if (crc_byte == byteArray[11])
                        {
                            if (byteArray[0] == 0xAA)
                            {
                                //onsole.WriteLine(byteArray[0].ToString("X"));
                                switch (byteArray[1])
                                {
                                    case 0x00:
                                        // Position variables (integer)
                                        int x_pos = 0, y_pos = 0, z_pos = 0;

                                        // Position variables (double)
                                        double x = 0, y = 0, z = 0;

                                        // Velocity variables (double)
                                        double x_vel = 0, y_vel = 0, z_vel = 0;

                                        // Jacobian matrices
                                        double[,] Jacobi_plus = new double[5, 3];
                                        double[,] Jacobi_vel = new double[3, 1];

                                        // Omega array
                                        double[] omega = new double[5];

                                        // Omega placeholders
                                        double omega1_plc = 0.0, omega2_plc = 0.0, omega3_plc = 0.0, omega4_plc = 0.0, omega5_plc = 0.0;

                                        x_pos = CombineBytesToInt32(byteArray[2], byteArray[3], byteArray[4]);
                                        y_pos = CombineBytesToInt32(byteArray[5], byteArray[6], byteArray[7]);
                                        z_pos = CombineBytesToInt32(byteArray[8], byteArray[9], byteArray[10]);

                                        x_vel = CombineBytesToInt16Vel(byteArray[12], byteArray[13]) / 10.0; // cm/s
                                        y_vel = CombineBytesToInt16Vel(byteArray[14], byteArray[15]) / 10.0;
                                        z_vel = CombineBytesToInt16Vel(byteArray[16], byteArray[17]) / 10.0;

                                        if (x_pos >= 0x800000)
                                        {
                                            x_pos = (x_pos - 0x800000);
                                            x_pos = (-1) * x_pos;
                                        }
                                        x = x_pos / 10000.0;

                                        if (y_pos >= 0x800000)
                                        {
                                            y_pos = (y_pos - 0x800000);
                                            y_pos = (-1) * y_pos;
                                        }
                                        y = y_pos / 10000.0;

                                        if (z_pos >= 0x800000)
                                        {
                                            z_pos = (z_pos - 0x800000);
                                            z_pos = (-1) * z_pos;
                                        }
                                        z = z_pos / 10000.0;

                                        x = x * 20;
                                        y = y * 20;
                                        z = z * 15 + 700;
                                        int ret;
                                        double t1, t2, t3, t4, t5;

                                        (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                                        Jacobi_plus = CreateJacobianMatrix(t1 * Math.PI / 180.0, t2 * Math.PI / 180.0, t3 * Math.PI / 180.0, t4 * Math.PI / 180.0, t5 * Math.PI / 180.0);
                                        Jacobi_vel = CreateVelocityMatrix(x_vel, y_vel, z_vel);
                                        omega = MultiplyMatrices(Jacobi_plus, Jacobi_vel);
                                        omega1_plc = omega[0] * 1800 * 15 / Math.PI + 100.0;
                                        omega2_plc = omega[1] * 1800 * 5 / Math.PI + 50.0;
                                        omega3_plc = omega[2] * 1800 * 5 / Math.PI + 50.0;
                                        omega4_plc = -(omega[1] + omega[2]) * 1800 * 20 / Math.PI + 100.0;
                                        omega5_plc = 0.0; 


                                        ret = Check_angle(t1, t2, t3, t4, t5);

                                        if (ret == 0)
                                        {

                                           /* Anti wind-up */
                                           if (Math.Abs(omega1_plc) >= 500)
                                           {
                                               omega1_plc = 500;
                                           }
                                           if (Math.Abs(omega2_plc) >= 300)
                                           {
                                               omega2_plc = 300;
                                           }
                                           if (Math.Abs(omega3_plc) >= 300)
                                           {
                                               omega3_plc = 300;
                                           }

                                           if (write_csv == true)
                                           {
                                               using (StreamWriter writer = new StreamWriter(filePath, true))
                                               {
                                                   // string csvLine = $"{x},{y},{z},{x_vel},{y_vel},{z_vel}";
                                                   string csvLine = $"{x},{y},{z}";
                                                   writer.WriteLine(csvLine);
                                               }
                                           }

                                           //plot(Math.Abs(omega1_plc), Math.Abs(omega2_plc), Math.Abs(omega3_plc));
                                           //plot(x_vel);
                                           //scatter(x, y);
                                           //Console.WriteLine("---");

                                        //    plot(x, y, z);

                                           //plot(Math.Abs(omega[0]) * 5, Math.Abs(omega[1]) * 5, Math.Abs(omega[2] * 5));


                                           adaptive_runtime(x, y, z, Math.Abs(omega1_plc), Math.Abs(omega2_plc), Math.Abs(omega3_plc), Math.Abs(omega4_plc), Math.Abs(omega5_plc), glove_enable);
                                        }
                                    // plot(x, y, z);
                                    break;
                                    default:
                                    Console.WriteLine("Hex value is not 0x1, 0x2, 0xA, or 0xB");
                                    break;
                                }
                            }
                       }
                    }
                }
            }
        }
        #endregion

        private void adaptive_runtime(double x, double y, double z, double v1, double v2, double v3, double v4, double v5, int enable_stt)
        {
            int[] temp_vel = new int[5];
            double t1_adapt, t2_adapt, t3_adapt, t4_adapt, t5_adapt;
            int ret = 0;
            int movepath_status;

            /* Read status of Brake and AC Servo */
            ret = PLCReadbit(Constants.MOVEL_PATH, out movepath_status);


            ///* đang bỏ qua điều kiện Z --> Phải nhớ để add vô sau */
            ////---------------------------------
            (t1_adapt, t2_adapt, t3_adapt, t4_adapt, t5_adapt) = convert_position_angle(x, y, z);
            int[] temp_value = new int[5];
            if (enable_stt == 1)
            {
                int[] value_angle = new int[10];
                /* Run */
                temp_value[0] = (int)(Convert.ToDouble(t1_adapt) * 100000 + 18000000);
                temp_value[1] = (int)(Convert.ToDouble(t2_adapt - 90) * 100000 + 18000000);
                temp_value[2] = (int)(Convert.ToDouble(t3_adapt + 90) * 100000 + 18000000);
                temp_value[3] = (int)(Convert.ToDouble(t4_adapt + 90) * 100000 + 18000000);
                temp_value[4] = (int)(Convert.ToDouble(t5_adapt) * 100000 + 18000000);

                temp_vel[0] = (int)(Convert.ToDouble(v1) * 1000);
                temp_vel[1] = (int)(Convert.ToDouble(v2) * 1000);
                temp_vel[2] = (int)(Convert.ToDouble(v3) * 1000);
                temp_vel[3] = (int)(Convert.ToDouble(v4) * 1000);
                temp_vel[4] = (int)(Convert.ToDouble(v5) * 1000);
                /* Write the angle and velocity */
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(2100 + 2 * ind, temp_vel[ind]);
                }
                turn_on_1_pulse_relay(650);
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(1400 + 2 * ind, temp_value[ind]);
                }
            }
        }

        #region frame_calculation
        static byte Lc709204fCalculateCrc8Atm(byte[] data, ushort length)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            byte crc = 0;
            for (ushort i = 0; i < length; i++)
            {
                crc ^= data[i];

                for (byte bit = 0; bit < 8; bit++)
                {
                    if ((crc & 0x80) != 0)
                    {
                        crc = (byte)((crc << 1) ^ 0x07);
                    }
                    else
                    {
                        crc <<= 1;
                    }
                }
            }

            return crc;
        }
        static int CombineBytesToInt32(byte byte1, byte byte2, byte byte3)
        {
            // Combine the bytes into an int (32-bit)
            // The last byte is assumed to be 0 (most significant byte of the 32-bit integer)
            int combined = (byte1 << 16) | (byte2 << 8) | byte3;
            return combined;
        }
        static Int16 CombineBytesToInt16Vel(byte byte1, byte byte2)
        {
            // Combine the bytes into an int (16-bit)
            // The first byte is the most significant byte
            Int16 combined = (Int16)((byte1 << 8) | byte2);
            return combined;
        }
        #endregion

        #region button_function

        private async void Glove_connect_button_Click(object sender, RoutedEventArgs e)
        {

            uart = new SerialPort();
            uart.PortName = com_port_list1.Text;
            // Set baud rate
            int baudRate;
            if (int.TryParse(com_port_list2.Text, out baudRate))
            {
                uart.BaudRate = baudRate;
            }
            else
            {
                // Handle invalid baud rate input
                MessageBox.Show("Invalid baud rate input.");
            }
            // Set data bit
            int databit;
            if (int.TryParse(com_port_list3.Text, out databit))
            {
                uart.DataBits = databit;
            }
            else
            {
                // Handle invalid baud rate input
                MessageBox.Show("Invalid data bit input.");
            }

            // Set stop bits
            if (Enum.TryParse<StopBits>(com_port_list4.Text, out StopBits stopBits))
            {
                uart.StopBits = stopBits;
            }
            else
            {
                // Handle invalid stop bits input
                // For example: Display a message box informing the user
                MessageBox.Show("Invalid stop bits input.");
            }
            // Set parity
            if (Enum.TryParse<Parity>(com_port_list5.Text, out Parity parity))
            {
                uart.Parity = parity;
            }
            else
            {
                // Handle invalid parity input
                MessageBox.Show("Invalid parity input.");
            }
            progressbar1.Value = 100;
            uart.Open();

            if (!_uartWorker.IsBusy)
            {
                _uartWorker.RunWorkerAsync();
            }
        }
        private void Glove_disconnect_button_Click(object sender, RoutedEventArgs e)
        {
            uart.Close();
            progressbar1.Value = 0;
            if (_uartWorker.IsBusy)
            {
                _uartWorker.CancelAsync();
            }
        }
        private void EStop_bttn_Click(object sender, RoutedEventArgs e)
        {
            /* Stop command axis 1 */
            turn_on_1_pulse_relay(3200);
            /* Stop command axis 2 */
            turn_on_1_pulse_relay(3220);
            /* Stop command axis 3 */
            turn_on_1_pulse_relay(3240);
            /* Stop command axis 4 */
            turn_on_1_pulse_relay(3260);
            /* Stop command axis 5 */
            turn_on_1_pulse_relay(3280);
        }
        private void Glove_enable_button_Click(object sender, RoutedEventArgs e)
        {
            /* Reset error */
            turn_on_1_pulse_relay(3200);
            /* Turn on relay */
            turn_on_1_pulse_relay(600);
            glove_enable = 1;
        }
        private void Glove_disable_button_Click(object sender, RoutedEventArgs e)
        {
            glove_enable = 0;
        }
        private void Glove_refresh_button_Click(object sender, RoutedEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            com_port_list1.ItemsSource = ports;
        }
        private void Clr_Traj_btn_Click(object sender, RoutedEventArgs e)
        {
            RemoveSphereVisuals();
        }
        private void Open_menu_Click(object sender, RoutedEventArgs e)
        {
            vel_1_test = Convert.ToInt32(spd_tb.Text) * 1000;
            /* Write the vel */
            for (int ind = 0; ind < 5; ind++)
            {
                write_d_mem_32_bit(2100 + 2 * ind, vel_1_test);
            }
            /* Change vel relay */
            turn_on_1_pulse_relay(650);
        }
        private void Write_csv_Click(object sender, RoutedEventArgs e)
        {
            write_csv = true;
        }
        private void Unwrite_csv_Click(object sender, RoutedEventArgs e)
        {
            write_csv = false;
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Modify_dat_csv_Click(object sender, RoutedEventArgs e)
        {
            ModifyTrajectory(filePath, savePath);
        }
        private void Test_move_mod_Click(object sender, RoutedEventArgs e)
        {
            move = 2;
            // Initialize a 2D array to hold the CSV data
            // Assuming you know the size of the array (10 rows and number of columns as per your data)
            double[,] data = new double[10, 3];

            // Read the file and parse the data
            using (StreamReader sr = new StreamReader(savePath))
            {
                string line;
                int row = 0;

                while ((line = sr.ReadLine()) != null && row < 10)
                {
                    string[] values = line.Split(',');

                    for (int col = 0; col < values.Length; col++)
                    {
                        // Trim whitespace and convert to double
                        data[row, col] = double.Parse(values[col].Trim());
                        Console.WriteLine(data[row, col]);
                    }
                    row++;
                }
            }
            Move_mod_Function(data, "D1010");
        }

        #endregion

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
                return;
            }
            /* Reverse bit: status == 1 */
            ret = PLCWritebit(device_str, (~status) & 0x01);
            if (ret != 0)
            {
                return;
            }
            PLCReadbit(device_str, out readbit);

            /* Reverse it into the initial state */
            ret = PLCWritebit(device_str, (~(~status)) & 0x01);
            if (ret != 0)
            {
                return;
            }
            PLCReadbit(device_str, out readbit);
        }

        #region plot_data

        private void plot(double x, double y, double z)
        {
            // Update data points
            var timestamp = DateTime.Now;
            var dataPoint1 = new DataPoint(DateTimeAxis.ToDouble(timestamp), x);
            var dataPoint2 = new DataPoint(DateTimeAxis.ToDouble(timestamp), y);
            var dataPoint3 = new DataPoint(DateTimeAxis.ToDouble(timestamp), z);

            // Update series
            var series_x_pos = (LineSeries)_plotModel_position.Series[0];
            var series_y_pos = (LineSeries)_plotModel_position.Series[1];
            var series_z_pos = (LineSeries)_plotModel_position.Series[2];
            series_x_pos.Points.Add(dataPoint1);
            series_y_pos.Points.Add(dataPoint2);
            series_z_pos.Points.Add(dataPoint3);

            // Limit number of data points to keep plot responsive
            if (series_x_pos.Points.Count > 100)
            {
                series_x_pos.Points.RemoveAt(0);
                series_y_pos.Points.RemoveAt(0);
                series_z_pos.Points.RemoveAt(0);
            }

            // Refresh plot view
            plotView_position.InvalidatePlot();
        }

        private void scatter(double x, double y)
        {
            // Xóa các điểm dữ liệu cũ
            // scatterSeries.Points.Clear();

            var _scatterSeries_robot_pos = (ScatterSeries) _plotModel_robot_pos.Series[0];

            // Thêm điểm mới
            _scatterSeries_robot_pos.Points.Add(new ScatterPoint(x, y));

            // Cập nhật đồ thị
            plotView_robot_pos.InvalidatePlot(true);
        }

        #endregion

        static void CreateCSV(string filePath, string[] data)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (string line in data)
                {
                    // Write each line of data to the CSV file
                    writer.WriteLine(line);
                }
            }
        }
        private Point3D[] ReadDataFromCsv(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<CsvPoint>().ToList();
                return records.Select(r => new Point3D(r.X, r.Y, r.Z)).ToArray();
            }
        }

        #region polynomial_regression

        private double EvalPoly(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }
        private double[] PolyFit(double[] x, double[] y, int degree)
        {
            var vandermonde = new DenseMatrix(x.Length, degree + 1);
            var coefficients = new double[degree + 1];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    vandermonde[i, j] = Math.Pow(x[i], j);
                }
            }
            var yVector = new DenseVector(y);
            var coefficientsVector = vandermonde.QR().Solve(yVector);
            return coefficientsVector.ToArray();
        }
        public class CsvPoint
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        private IEnumerable<Point3D[]> SplitDataIntoSegments(Point3D[] data, int segmentSize)
        {
            for (int i = 0; i < data.Length; i += segmentSize)
            {
                yield return data.Skip(i).Take(segmentSize).ToArray();
            }
        }

        private IEnumerable<Point3D> ExtendData(Point3D[] data, int targetCount)
        {
            int originalCount = data.Length;
            int numSegments = (int)Math.Ceiling((double)targetCount / originalCount);
            int segmentSize = originalCount * numSegments;
            var extendedData = new List<Point3D>();

            for (int i = 0; i < numSegments; i++)
            {
                foreach (var point in data)
                {
                    extendedData.Add(point);
                }
            }

            return extendedData.Take(targetCount);
        }

        private void SaveRegressionResultToCsv(List<Point3D> allPoints, string filePath, int size)
        {

            // Chuyển đổi List<Point3D> thành Point3D[]
            Point3D[] data = allPoints.ToArray();

            // Số điểm mục tiêu (300 điểm)
            int targetCount = size;
            int originalCount = data.Length;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HasHeaderRecord = false; // Thiết lập HasHeaderRecord tại đây

            if (originalCount > targetCount)
            {
            }
            else
            {
                int delta = 1;
                while ((originalCount + delta) != targetCount) { delta += 1; }; 
                // Tăng số lượng điểm để đạt 300 điểm bằng cách chia nhỏ dữ liệu
                int segmentSize = originalCount + delta;
                var extendedData = ExtendData(data, segmentSize);

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config)) // Sử dụng config đã thiết lập
                {
                    foreach (var point in extendedData)
                    {
                        csv.WriteRecord(point); // Ghi mỗi điểm vào file CSV
                        csv.NextRecord();
                    }
                }
            }
        }

        private void ModifyTrajectory(string csvFilePath, string savepath)
        {

            // Đọc dữ liệu từ tệp CSV
            var data = ReadDataFromCsv(csvFilePath);

            var length_data = data.Length;
            // Chia dữ liệu thành các đoạn nhỏ hơn, ví dụ: mỗi đoạn chứa 30 điểm
            int segmentSize = 30;
            if (segmentSize == length_data)
            {
                segmentSize = segmentSize + 1;
            }
            var segments = SplitDataIntoSegments(data, segmentSize);

            // Khởi tạo một danh sách để tích lũy tất cả các điểm
            List<Point3D> allPoints = new List<Point3D>();

            foreach (var segment in segments)
            {
                // Tạo biến t (biến tham số)
                double[] t = Enumerable.Range(0, segment.Length).Select(i => (double)i / (segment.Length - 1)).ToArray();

                // Tách các biến độc lập và biến phụ thuộc
                double[] X = segment.Select(p => p.X).ToArray();
                double[] Y = segment.Select(p => p.Y).ToArray();
                double[] Z = segment.Select(p => p.Z).ToArray();

                // Hồi quy đa thức bậc 10 cho từng biến x, y, z theo t
                double[] coeffsX = PolyFit(t, X, 10);
                double[] coeffsY = PolyFit(t, Y, 10);
                double[] coeffsZ = PolyFit(t, Z, 10);

                // Tạo giá trị dự đoán cho x, y, z
                int numPoints = 10;
                double[] tFit = Enumerable.Range(0, numPoints).Select(i => (double)i / (numPoints - 1)).ToArray();
                Point3D[] curve = tFit.Select(tt => new Point3D(
                    EvalPoly(coeffsX, tt),
                    EvalPoly(coeffsY, tt),
                    EvalPoly(coeffsZ, tt))).ToArray();

                // Thêm các điểm của segment vào danh sách tất cả các điểm
                allPoints.AddRange(curve);
            }
            SaveRegressionResultToCsv(allPoints, savepath, 10);
        }
        #endregion
    }

}
#endif