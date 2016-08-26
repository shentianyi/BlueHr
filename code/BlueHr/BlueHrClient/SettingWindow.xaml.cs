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
using System.Windows.Shapes;

namespace BlueHrClient
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : MetroWindow
    {
        public SettingWindow()
        {
            InitializeComponent();
        }
        private void saveAddClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
                this.pathTextBox.Text = fbd.SelectedPath;
        }

        private void msgShowBoxChecked(object sender, RoutedEventArgs e)
        {
            autoCheckinBox.IsChecked = !msgShowBox.IsChecked;
        }

        private void autoCheckinBoxChecked(object sender, RoutedEventArgs e)
        {
            msgShowBox.IsChecked = !autoCheckinBox.IsChecked;
        }

    }
}
