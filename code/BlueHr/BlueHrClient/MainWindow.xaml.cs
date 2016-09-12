﻿using Brilliantech.Framwork.Utils.LogUtil;
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
using System.IO;
using System.Runtime.InteropServices;
using BlueHrLib.Service.Interface;
using BlueHrClient.Properties;
using BlueHrLib.Data;
using System.Windows.Threading;
using BlueHrLib.Data.Model;
using System.Media;
using BlueHrClient.Escape;
using System.Drawing;


namespace BlueHrClient
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        SoundPlayer player = new SoundPlayer("alarm.WAV");
        Escaper escape = new Escaper();
        public string fileNameForUpdate; 
        public static string[] withErrorMessage = new string[2];

        public MainWindow()
        {
            InitializeComponent();
            
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct IDCardData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name; //姓名   
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string Sex;   //性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string Nation; //名族
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string Born; //出生日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
            public string Address; //住址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string IDCardNo; //身份证号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string GrantDept; //发证机关
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeBegin; // 有效开始日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeEnd;  // 有效截止日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string reserved; // 保留
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string PhotoFileName; // 照片路径
        }


        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_FindReader", CharSet = CharSet.Ansi)]
        public static extern int Syn_FindReader();
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        public static extern int Syn_OpenPort(int iPort);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetMaxRFByte", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetMaxRFByte(int iPort, byte ucByte, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_StartFindIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_StartFindIDCard(int iPort, ref byte pucIIN, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SelectIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_SelectIDCard(int iPort, ref byte pucSN, int iIfOpen);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_ReadBaseMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadBaseMsg(int iPort, IntPtr pucCHMsg, ref uint puiCHMsgLen, IntPtr pucPHMsg, ref uint puiPHMsgLen, int iIfOpen);

        /***********************设置附加功能函数 ************************/
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoPath", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoPath(int iOption, ref byte cPhotoPath);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetPhotoName", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetPhotoName(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetSexType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetSexType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetNationType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetNationType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetBornType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetBornType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeBType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetUserLifeBType(int iType);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_SetUserLifeEType", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetUserLifeEType(int iType, int iOption);
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_StrBase64ToPhoto", CharSet = CharSet.Ansi)]
        public static extern int Syn_StrBase64ToPhoto(string cBase64, int iLen, string cPhotoName);

        int type = 0;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();
            timer.Interval = new TimeSpan(0, 0, 1);   //间隔1秒
            timer.Tick += new EventHandler(selectToReader);
            timer.Start();
        }


        private void Window_Closing(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void selectToReader(object sender, EventArgs e)
        {
            try
            {
                string stmp;
                int i, nPort;
                uint[] iBaud = new uint[1];
                byte[] pucIIN = new byte[4];
                byte[] pucSN = new byte[8];
                i = Syn_FindReader();
                if (i > 0)
                {
                    stmp = Convert.ToString(i);
                    nPort = Convert.ToInt32(stmp);
                    BaseConfig.NPort = nPort;
                    if (Syn_OpenPort(nPort) == 0)
                    {
                        if (Syn_SetMaxRFByte(nPort, 80, 0) == 0)
                        {
                            readIDCard();
                        }
                    }
                    else
                    {
                        stmp = Convert.ToString(System.DateTime.Now) + "  打开端口失败";
                        MessageBox.Show(stmp);
                    }
                }
                else
                {
                    stmp = Convert.ToString(System.DateTime.Now) + "  没有找到读卡器";
                    MessageBox.Show(stmp);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("异常", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void loadData()
        {
            if (!System.IO.Directory.Exists(BaseConfig.SavePathPhoto))
                System.IO.Directory.CreateDirectory(BaseConfig.SavePathPhoto);

            if (!System.IO.Directory.Exists(BaseConfig.SavePathPhoto+"\\tmp"))
                System.IO.Directory.CreateDirectory(BaseConfig.SavePathPhoto+"\\tmp");

            if (!System.IO.Directory.Exists(BaseConfig.SavePath))
                System.IO.Directory.CreateDirectory(BaseConfig.SavePath);
            player.LoadAsync();
            byte[] cPath = new byte[255];
            cPath = System.Text.Encoding.Default.GetBytes(BaseConfig.SavePathPhoto);
            Syn_SetPhotoPath(2, ref cPath[0]);
            // Syn_SetPhotoType(1);
            Syn_SetPhotoName(3);
            Syn_SetSexType(1);
            Syn_SetNationType(2);
            Syn_SetBornType(2);
            Syn_SetUserLifeBType(2);
            Syn_SetUserLifeEType(2,0);
        }
        private void setter_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow win = new SettingWindow();
            win.ShowDialog();
           
        }
        private void readIDCard()
        {
            try
            {
                string[] files = Directory.GetFiles(BaseConfig.SavePathPhoto + "\\tmp");
                foreach (var f in files)
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch { }
                }
            }
            catch (Exception ex) { }
            IDCardData CardMsg = new IDCardData();
            string[] stmp = new string[11];
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            Syn_StartFindIDCard(BaseConfig.NPort, ref pucIIN[0], 0);
            Syn_SelectIDCard(BaseConfig.NPort, ref pucSN[0], 0);
            try
            {
                int ii = Syn_ReadMsg(BaseConfig.NPort, 0, ref CardMsg);
                if (ii == 0)
                {
                    Status.Text = "读取成功";
                    Name.Text = CardMsg.Name.Trim();
                    Sex.Text = CardMsg.Sex;
                    Nation.Text = CardMsg.Nation;
                    Born.Text = CardMsg.Born;
                    Address.Text = CardMsg.Address;
                    ID.Text = CardMsg.IDCardNo;
                    GrantDept.Text = CardMsg.GrantDept;
                    UserLifeBegin.Text = CardMsg.UserLifeBegin;
                    Link.Text = "--";
                    UserLifeEnd.Text = CardMsg.UserLifeEnd;

                    string fileName = System.IO.Path.Combine(BaseConfig.SavePathPhoto, string.Format("{0}_{1}.bmp", CardMsg.Name.Trim(), CardMsg.IDCardNo));
                    string newFileName = string.Format("{0}_{1}_{2}.bmp", Guid.NewGuid().ToString(), CardMsg.Name.Trim(), CardMsg.IDCardNo);
                    string newPath = System.IO.Path.Combine(BaseConfig.SavePathPhoto, "tmp", newFileName);
                    File.Copy(fileName, newPath);
                    fileNameForUpdate = newPath;
                    photo.Source = new BitmapImage(new Uri(newPath, UriKind.Absolute));

                    if (referForCheck())
                    {
                        msgBlock.Text = "通 过";
                        msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
                    }
                    else
                    {
                        msgBlock.Text = "";
                        //msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));                      
                    }

                    if (BaseConfig.SaveNotes)
                    {         
                        string timeForName = System.DateTime.Now.ToString("yyyy_MM_dd");
                        FileStream file = new FileStream(BaseConfig.SavePath + "\\" + timeForName + "记录.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter stingForWrite = new StreamWriter(file);
                        stmp[0] = Convert.ToString(System.DateTime.Now);
                        stmp[1] = "  姓名:" + Name.Text;
                        stmp[2] = "  性别:" + Sex.Text;
                        stmp[3] = "  民族:" + Nation.Text;
                        stmp[4] = "  身份证号:" + ID.Text;
                        stmp[5] = "  出生日期:" + Born.Text;
                        stmp[6] = "  地址:" + Address.Text;
                        stmp[7] = "  发证机关:" + GrantDept.Text;
                        stmp[8] = "  有效期开始:" + UserLifeBegin.Text;
                        stmp[9] = "  有效期结束:" + UserLifeEnd.Text;
                        for (int i = 0; i <= 9; i++)
                        {
                            stingForWrite.WriteLine(stmp[i]);
                        }
                        stingForWrite.Close();
                        file.Close();
                    }
                }
                else
                {
                    Status.Text = "请放卡...";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                //Marshal.FreeHGlobal(msg);
                //Marshal.FreeHGlobal(img);
            }
         
        }

        private bool referForCheck()
        {
            IStaffService staffService = new StaffService(Settings.Default.db);
            Staff staff = staffService.FindByStaffId(ID.Text);
   
            if (staff == null)
            {
                if (BaseConfig.Sound)
                {
                    player.Play();
                }
                MessageBoxResult result = MessageBox.Show("请前往网页端创建后扫描", "不存在该身份证信息", MessageBoxButton.OK, MessageBoxImage.Hand);
                return false;
            }
            else {
                if (staff.name != Name.Text)
                {
                    if (BaseConfig.Sound)
                    {
                        player.Play();
                    }
                    withErrorMessage[0] = staff.name;
                    withErrorMessage[1] = staff.id;
                    WarningWindow win = new WarningWindow();
                    win.ShowDialog();
                }
                else
                {
                    if (staff.isIdChecked)
                    {
                        msgBlock.Text = "通 过";
                        msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
                        return true;
                    }
                    else
                    {
                        if (BaseConfig.AutoCheckin)
                        {
                            if (upDate())
                            {
                                msgBlock.Text = "通 过";
                                msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
                                return true;
                            }
                            else
                            {
                                msgBlock.Text = "待更新";
                                msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));
                            }

                        }
                        else
                        {
                            // win.ShowDialog();
                            MessageBoxResult result = MessageBox.Show("       是否更新？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Hand);
                            if (result == MessageBoxResult.OK)
                            {
                                if (upDate())
                                {
                                    msgBlock.Text = "通 过";
                                    msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
                                    return true;
                                }
                                else
                                {
                                    msgBlock.Text = "待更新";
                                    msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

 
        private bool upDate()
        {
            IStaffService staffService = new StaffService(Settings.Default.db);
            StaffIdCard cardData = new StaffIdCard();
            cardData.id = ID.Text;
            cardData.name = Name.Text;
            cardData.sex = escape.SexEscapeForUpdate(Sex.Text);
            cardData.ethnic = Nation.Text;
            cardData.birthday = Convert.ToDateTime(Born.Text);
            cardData.residenceAddress = Address.Text;
            cardData.effectiveFrom = Convert.ToDateTime(UserLifeBegin.Text);
            cardData.effectiveEnd = Convert.ToDateTime(UserLifeEnd.Text);
            cardData.institution = GrantDept.Text;
            cardData.photo = "data:image/jpg;base64,"+ photoToString();
            return staffService.CheckStaffAndUpdateInfo(cardData);
        }

        private string photoToString()
        {

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = System.IO.File.OpenRead(fileNameForUpdate);
            bitmapImage.EndInit();
            photo.Source = bitmapImage;
            byte[] imageData = new byte[bitmapImage.StreamSource.Length];
            bitmapImage.StreamSource.Seek(0, System.IO.SeekOrigin.Begin);
            bitmapImage.StreamSource.Read(imageData, 0, imageData.Length);
            //string photoString = System.Text.Encoding.Default.GetString(imageData);
            string photoString = Convert.ToBase64String(imageData);
            return photoString;
        }
    }
}