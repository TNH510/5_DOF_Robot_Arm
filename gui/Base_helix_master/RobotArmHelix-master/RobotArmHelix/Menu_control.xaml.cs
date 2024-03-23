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
using System.Windows.Shapes;

namespace RobotArmHelix
{
    /// <summary>
    /// Interaction logic for Menu_control.xaml
    /// </summary>
    public partial class Menu_control : Window
    {
        private MainWindow mainWindow; // Reference to MainWindow
        public Menu_control(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow; // Store the reference to MainWindow
        }
    }
}
