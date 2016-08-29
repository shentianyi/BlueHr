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
using BlueHrClient.Config;

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
            Config();

        }
        public void Config()
        {
            autoCheckinBox.IsChecked = BaseConfig.AutoCheckin;
            msgShowBox.IsChecked = !autoCheckinBox.IsChecked;
            timeTextBox.IsEnabled = (bool)msgShowBox.IsChecked;
            timeTextBox.Text = BaseConfig.TimeForMsg.ToString();
            pathTextBox.Text = BaseConfig.SavePath;
            saveNotes.IsChecked = BaseConfig.SaveNotes;
            pathTextBox.IsEnabled = BaseConfig.SaveNotes;
            saveButton.IsEnabled = BaseConfig.SaveNotes;
        }
        private void saveAddClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                this.pathTextBox.Text = fbd.SelectedPath;
                BaseConfig.SavePath = this.pathTextBox.Text;
            }
        }

        private void msgShowBoxChecked(object sender, RoutedEventArgs e)
        {
            autoCheckinBox.IsChecked = !msgShowBox.IsChecked;
            BaseConfig.AutoCheckin = false;
            timeTextBox.IsEnabled = true;
        }

        private void autoCheckinBoxChecked(object sender, RoutedEventArgs e)
        {
            msgShowBox.IsChecked = !autoCheckinBox.IsChecked;
            BaseConfig.AutoCheckin = true;
            timeTextBox.IsEnabled = false;
        }

        private void saveNotesSelected(object sender, RoutedEventArgs e)
        {
            BaseConfig.SaveNotes = (bool)saveNotes.IsChecked;
            pathTextBox.IsEnabled = true;
            saveButton.IsEnabled = true;
        }

        private void saveNotesUnSelected(object sender, RoutedEventArgs e)
        {
            BaseConfig.SaveNotes = (bool)saveNotes.IsChecked;
        }
    }
}
