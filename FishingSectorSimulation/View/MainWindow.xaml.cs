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

namespace FishingSectorSimulation.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
            startbtn.Click += (sender, e) =>
            {
                if (flag == false)
                {
                    flag = true;
                    startbtn.Content = "Stop Simulation";
                }
                else {
                    flag = false;
                    startbtn.Content = "Start Simulation";
                }
            };
        }
    }
}
