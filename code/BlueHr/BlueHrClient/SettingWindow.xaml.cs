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
using System.Runtime.InteropServices;

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
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindReader", CharSet = CharSet.Ansi)]
        public static extern int Syn_FindReader();
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        public static extern int Syn_OpenPort(int iPort);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ResetSAM", CharSet = CharSet.Ansi)]
        public static extern int Syn_ResetSAM(int iPort, int iIfOpen);
        public void Config()
        {
            autoCheckinBox.IsChecked = BaseConfig.AutoCheckin;
            msgShowBox.IsChecked = !autoCheckinBox.IsChecked;
            timeTextBox.IsEnabled = (bool)msgShowBox.IsChecked;
            timeTextBox.Text = BaseConfig.TimeForMsg.ToString();
            filePath.Text = BaseConfig.SavePath;
            photoPath.Text = BaseConfig.SavePathPhoto;
            saveNotes.IsChecked = BaseConfig.SaveNotes;
            soundBox.IsChecked = BaseConfig.Sound;
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
        }

        private void saveNotesUnSelected(object sender, RoutedEventArgs e)
        {
            BaseConfig.SaveNotes = (bool)saveNotes.IsChecked;
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定要复位？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                int nRet, nPort;
                string stmp;
                nPort = Convert.ToInt32(Syn_FindReader());
                if (Syn_OpenPort(nPort) == 0)
                {
                    nRet = Syn_ResetSAM(nPort, 0);
                    if (nRet == 0)
                    {
                        stmp = Convert.ToString(System.DateTime.Now) + " 复位SAM模块成功";
                        MessageBox.Show("复位SAM模块成功");
                    }
                    else
                    {
                        stmp = Convert.ToString(System.DateTime.Now) + " 复位SAM模块失败";
                        MessageBox.Show("复位SAM模块失败");
                    }
                }
                else
                {
                    stmp = Convert.ToString(System.DateTime.Now) + "  打开端口失败";
                    MessageBox.Show("打开端口失败");
                }

            }
            if (result == MessageBoxResult.No)
            {
                this.Close();
            }
            
        }

        private void filePathClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                filePath.Text = fbd.SelectedPath;
                BaseConfig.SavePath = filePath.Text;
            }
        }

        private void photoPathClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                photoPath.Text = fbd.SelectedPath;
                BaseConfig.SavePathPhoto = photoPath.Text;
            }

        }

        private void soungChecked(object sender, RoutedEventArgs e)
        {
            BaseConfig.Sound = true;
        }

        private void soundUnChecked(object sender, RoutedEventArgs e)
        {
            BaseConfig.Sound = false;
        }
    }
}
