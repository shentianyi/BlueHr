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
            if (!System.IO.Directory.Exists(BaseConfig.SavePathPhoto))
                System.IO.Directory.CreateDirectory(BaseConfig.SavePathPhoto);
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
            IDCardData CardMsg = new IDCardData();
            string[] stmp = new string[11];
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            Syn_StartFindIDCard(BaseConfig.NPort, ref pucIIN[0], 0);
            Syn_SelectIDCard(BaseConfig.NPort, ref pucSN[0], 0);
            //MessageBox.Show(Syn_ReadMsg(1001, 1, ref CardMsg).ToString());

            string cardMsg = new string(' ', 256);  //身份证基本信息返回长度为256
            string imgMsg = new string(' ', 1024);  //身份证图片信息返回长度为1024
            IntPtr msg = Marshal.StringToHGlobalAnsi(cardMsg);  //身份证基本信息
            IntPtr img = Marshal.StringToHGlobalAnsi(imgMsg);   //身份证图片
            //byte[] img = new byte[1024];
            try
            {
                uint mLen = 0;
                uint iLen = 0;

                //System.Diagnostics.Debug.Assert(Syn_ReadBaseMsg(BaseConfig.NPort, msg, ref mLen, img, ref iLen, 0) == 0);

                if (Syn_ReadBaseMsg(BaseConfig.NPort, msg, ref mLen, img, ref iLen, 0) == 0)
                {
                    string card = Marshal.PtrToStringUni(msg);
                    char[] cartb = card.ToCharArray();
                    CardMsg.Name = (new string(cartb, 0, 15)).Trim();
                    CardMsg.Sex = new string(cartb, 15, 1);
                    CardMsg.Nation = new string(cartb, 16, 2);
                    CardMsg.Born = new string(cartb, 18, 8);
                    CardMsg.Address = (new string(cartb, 26, 35)).Trim();
                    CardMsg.IDCardNo = new string(cartb, 61, 18);
                    CardMsg.GrantDept = (new string(cartb, 79, 15)).Trim();
                    CardMsg.UserLifeBegin = new string(cartb, 94, 8);
                    CardMsg.UserLifeEnd = new string(cartb, 102, 8);
                    Status.Text = "读取成功";
                    Name.Text = CardMsg.Name;
                    Sex.Text = escape.SexEscape(CardMsg.Sex);
                    Nation.Text = escape.NationEscape(CardMsg.Nation);
                    Born.Text = escape.DateEscape(CardMsg.Born);
                    Address.Text = CardMsg.Address;
                    ID.Text = CardMsg.IDCardNo;
                    GrantDept.Text = CardMsg.GrantDept;
                    UserLifeBegin.Text = escape.DateEscape(CardMsg.UserLifeBegin);
                    UserLifeEnd.Text = escape.DateEscape(CardMsg.UserLifeEnd);
                    Link.Text = "--";
                    if (referForCheck())
                    {
                        msgBlock.Text = "通 过";
                        msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));

                    }
                    else {
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
                MessageBox.Show(e.ToString());
            }
            finally
            {
                Marshal.FreeHGlobal(msg);
                Marshal.FreeHGlobal(img);
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
                    return false;
                }
                else
                {
                    if (!BaseConfig.AutoCheckin)
                    {
                        MessageBoxResult result = MessageBox.Show("信息核对无误", "正确", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                    return true;

                }
            }
        }

        //private void Checkin()
        //{
        //    IStaffService staffService = new StaffService(Settings.Default.db);
        //    Staff staff = staffService.FindByStaffId(ID.Text);
        //    if (staff == null)
        //    {
        //        if (BaseConfig.Sound)
        //        {
        //            player.Play();
        //        }
                
 

        //            // win.ShowDialog();
        //            MessageBoxResult result = MessageBox.Show("员工中不存在该用户信息，请创建后扫描", "错误", MessageBoxButton.OK, MessageBoxImage.Hand);
        //        //    if (result == MessageBoxResult.OK)
        //        //    {
        //        //        if (getData())
        //        //        {
        //        //            msgBlock.Text = "通 过";
        //        //            msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
        //        //        }
        //        //        else
        //        //        {
        //        //            msgBlock.Text = "不存在";
        //        //            msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));
        //        //        }
        //        //}
        //    }
        //    else {
        //        if (staff.isIdChecked)
        //        {
        //            msgBlock.Text = "通 过";
        //            msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
        //        }
        //        else
        //        {
        //            if (BaseConfig.AutoCheckin)
        //            {
        //                if (upDate())
        //                {
        //                    msgBlock.Text = "通 过";
        //                    msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
        //                }
        //                else
        //                {
        //                    msgBlock.Text = "待更新";
        //                    msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));
        //                }
        //            }
        //            else
        //            {
        //                // win.ShowDialog();
        //                MessageBoxResult result = MessageBox.Show("       是否更新？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Hand);
        //                if (result == MessageBoxResult.OK)
        //                {
        //                    if (upDate())
        //                    {
        //                        msgBlock.Text = "通 过";
        //                        msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#2786E4"));
        //                    }
        //                    else
        //                    {
        //                        msgBlock.Text = "待更新";
        //                        msgBlock.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#EA0000"));
        //                    }
        //                }
        //            }

        //        }
        //    }
        //}
        //private bool getData()
        //{
        //    IStaffService staffService = new StaffService(Settings.Default.db);
        //    StaffIdCard cardData = new StaffIdCard();
        //    cardData.id = ID.Text;
        //    cardData.name = Name.Text;
        //    cardData.sex = escape.SexEscapeForUpdate(Sex.Text);
        //    cardData.ethnic = Nation.Text;
        //    cardData.birthday = Convert.ToDateTime(Born.Text);
        //    cardData.residenceAddress = Address.Text;
        //    cardData.effectiveFrom = Convert.ToDateTime(UserLifeBegin.Text);
        //    cardData.effectiveEnd = Convert.ToDateTime(UserLifeEnd.Text);
        //    cardData.institution = GrantDept.Text;
        //    // cardData.photo = 
        //    return staffService.CreateInfoAndSetCheck(cardData);
        //}
        //private bool upDate()
        //{
        //    IStaffService staffService = new StaffService(Settings.Default.db);
        //    StaffIdCard cardData = new StaffIdCard();
        //    cardData.id = ID.Text;
        //    cardData.name = Name.Text;
        //    cardData.sex = escape.SexEscapeForUpdate(Sex.Text);
        //    cardData.ethnic = Nation.Text;
        //    cardData.birthday = Convert.ToDateTime(Born.Text);
        //    cardData.residenceAddress = Address.Text;
        //    cardData.effectiveFrom = Convert.ToDateTime(UserLifeBegin.Text);
        //    cardData.effectiveEnd = Convert.ToDateTime(UserLifeEnd.Text);
        //    cardData.institution = GrantDept.Text;
        //    return staffService.CheckStaffAndUpdateInfo(cardData); 
        //}

    }
}