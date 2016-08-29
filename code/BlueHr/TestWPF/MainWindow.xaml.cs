using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using TestWPF.Properties;

namespace TestWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void createStaffBtn_Click(object sender, RoutedEventArgs e)
        {
           
            for (int i = 0; i < 4000; i++)
            { IStaffService ss = new StaffService(Settings.Default.db);
            List<Staff> staffs = new List<Staff>();
                staffs.Add(new Staff() { nr = i.ToString(), id = i.ToString(), workStatus = (int)WorkStatus.OnWork, companyId = 1, departmentId = 1 });
                ss.Creates(staffs);
            }
           
        }
    }
}
