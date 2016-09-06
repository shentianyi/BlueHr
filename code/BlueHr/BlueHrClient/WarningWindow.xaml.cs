using BlueHrClient.Config;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace BlueHrClient
{
    /// <summary>
    /// WarningWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WarningWindow : Window
    {
        public WarningWindow()
        {
            InitializeComponent();
            Configure();

        }

        private void Configure()
        {

            ID.Text = MainWindow.withErrorMessage[1];
            Name.Text = MainWindow.withErrorMessage[0];
        }

        private void closeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
