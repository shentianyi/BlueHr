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
using System.IO;
using System.Runtime.InteropServices;
using BlueHrLib.Service.Interface;
using BlueHrClient.Properties;
using BlueHrLib.Data;
using System.Windows.Threading;
using BlueHrLib.Data.Model;
using System.Media;

namespace BlueHrClient
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        DispatcherTimer timer = new DispatcherTimer();
        SoundPlayer player = new SoundPlayer("alarm.WAV");
       

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
        [DllImport("SynIDCardAPI.dll", EntryPoint = "Syn_PhotoToStrBase64", CharSet = CharSet.Ansi)]
        public static extern int Syn_PhotoToStrBase64(ref byte cBase64, ref uint iLen);



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
                //配置
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

        private void loadData()
        {
            player.LoadAsync();
            byte[] cPath = new byte[255];
            cPath = System.Text.Encoding.Default.GetBytes(BaseConfig.SavePathPhoto);
            Syn_SetPhotoPath(2, ref cPath[0]);
            Syn_SetPhotoType(1);
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
            IDCardData CardMsg = new IDCardData();
            string[] stmp = new string[15];
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            Syn_StartFindIDCard(BaseConfig.NPort, ref pucIIN[0], 0);
            Syn_SelectIDCard(BaseConfig.NPort, ref pucSN[0], 0);
            if (Syn_ReadMsg(BaseConfig.NPort, 0, ref CardMsg) == 0)
            {

                Name.Text = CardMsg.Name;
                Sex.Text = CardMsg.Sex;
                Nation.Text = CardMsg.Nation;
                Born.Text = CardMsg.Born;
                Address.Text = CardMsg.Address;
                ID.Text = CardMsg.IDCardNo;
                GrantDept.Text = CardMsg.GrantDept;
                UserLifeBegin.Text = CardMsg.UserLifeBegin;
                UserLifeEnd.Text = CardMsg.UserLifeEnd;
                photo.Source = new BitmapImage(new Uri(CardMsg.PhotoFileName, UriKind.Absolute));
                RenderOptions.SetBitmapScalingMode(photo, BitmapScalingMode.Fant);
                Checkin();
                Status.Text = "读取成功";
                if (BaseConfig.SaveNotes)
                {
                    stmp[0] = Convert.ToString(System.DateTime.Now);
                    stmp[1] = "  姓名:" + CardMsg.Name;
                    stmp[2] = "  性别:" + CardMsg.Sex;
                    stmp[3] = "  民族:" + CardMsg.Nation;
                    stmp[4] = "  身份证号:" + CardMsg.IDCardNo;
                    stmp[5] = "  出生日期:" + CardMsg.Born;
                    stmp[6] = "  地址:" + CardMsg.Address;
                    stmp[7] = "  发证机关:" + CardMsg.GrantDept;
                    stmp[8] = "  有效期开始:" + CardMsg.UserLifeBegin;
                    stmp[9] = "  有效期结束:" + CardMsg.UserLifeEnd;
                    stmp[10] = "  照片文件名:" + CardMsg.PhotoFileName;
                    string timeForName = System.DateTime.Now.ToString("yyyy_MM_dd");
                    FileStream file = new FileStream(BaseConfig.SavePath + "\\" + timeForName + "记录.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter stingForWrite = new StreamWriter(file); 
                    for (int i = 0; i <= 9; i++)
                    {
                        stingForWrite.WriteLine(stmp[i]);
                    }
                    stingForWrite.Close();
                }
            }
            else
            {
                Status.Text = "请放卡...";
            }

        }
        private void Checkin()
        {
            WarningWindow win = new WarningWindow();
            IStaffService staffService = new StaffService(Settings.Default.db);
            Staff staff = staffService.FindByStaffId(ID.Text);
            if (staff == null)
            {
                if (BaseConfig.Sound)
                {
                    player.Play();
                }
                if (BaseConfig.AutoCheckin)
                {
                    if (getData())
                    {
                        msgBlock.Text = "ACCEPT";
                        msgBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2786E4"));
                    }
                    else {
                        msgBlock.Text = "ERROR";
                        msgBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EA0000"));
                    }

                   // byte[] cPath = new byte[255];
                    //uint[] iBaud = new uint[1];
                 
                   // if (Syn_PhotoToStrBase64(ref cPath[0],ref iBaud[0]) == 0)
                }
                else
                {
                    // win.ShowDialog();
                    MessageBoxResult result = MessageBox.Show("       是否同步？", "错误", MessageBoxButton.OKCancel, MessageBoxImage.Hand);
                    if (result == MessageBoxResult.OK)
                    {
                        if (getData())
                        {
                            msgBlock.Text = "ACCEPT";
                            msgBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2786E4"));
                        }
                        else
                        {
                            msgBlock.Text = "ERROR";
                            msgBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EA0000"));
                        }
                    }
                }
            }
            else {
                msgBlock.Text = "ACCEPT";
                msgBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2786E4"));
            }

        }
        private bool getData()
        {
            IStaffService staffService = new StaffService(Settings.Default.db);
            StaffIdCard cardData = new StaffIdCard();
            cardData.id = ID.Text;
            cardData.name = Name.Text;
            cardData.sex = Sex.Text;
            cardData.ethnic = Nation.Text;
            cardData.birthday = Convert.ToDateTime(Born.Text);
            cardData.residenceAddress = Address.Text;
            cardData.effectiveFrom = Convert.ToDateTime(UserLifeBegin.Text);
            cardData.effectiveEnd = Convert.ToDateTime(UserLifeEnd.Text);
            cardData.institution = GrantDept.Text;
            // cardData.photo = 
            return staffService.CheckStaffAndUpdateInfo(cardData); 
        }

    }
}