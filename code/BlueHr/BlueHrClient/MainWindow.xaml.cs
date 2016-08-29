using Brilliantech.Framwork.Utils.LogUtil;
using MahApps.Metro.Controls;
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
using BlueHrLib.Service.Implement;
using BlueHrClient.Config;

namespace BlueHrClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogUtil.Logger.Error("hello");
        }
        private void setter_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow win = new SettingWindow();
            win.ShowDialog();
           
        }

        private void buttonForTest(object sender, RoutedEventArgs e)
        {
             WarningWindow win = new WarningWindow();
            // if (StaffService.CheckStaffById(ID.Text))
            // {
            if (BaseConfig.AutoCheckin)
            {
                msgBlock.Text = "ACCEPT";
            }
            else
            {
                msgBlock.Text = "ERROR";
                win.ShowDialog();
            }
           // }
        }
    }
}
