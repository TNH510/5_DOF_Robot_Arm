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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RobotArmHelix.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        private MainWindow mainWindow; 
        public Customers()
        {
            InitializeComponent();
            mainWindow = new MainWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.MyFunction();
        }
    }
}
