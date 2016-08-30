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
            IDCardData CardMsg = new IDCardData();
            string stmp;
            int i, nRet,nPort;
            uint[] iBaud = new uint[1];
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            i = Syn_FindReader();
            if (i > 0)
            {
                stmp = Convert.ToString(i);
                nPort = Convert.ToInt32(stmp);
                if (Syn_OpenPort(nPort) == 0)
                {
                    if (Syn_SetMaxRFByte(nPort, 80, 0) == 0)
                    {
                        nRet = Syn_StartFindIDCard(nPort, ref pucIIN[0], 0);
                        nRet = Syn_SelectIDCard(nPort, ref pucSN[0], 0);
                        nRet = Syn_ReadMsg(nPort, 0, ref CardMsg);
                        if (nRet == 0)
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
                            // stmp = Convert.ToString(System.DateTime.Now) + "  照片文件名:" + CardMsg.PhotoFileName;

                            if (BaseConfig.SaveNotes)
                            {
                                string timeForName = System.DateTime.Now.ToString("yyyy_MM_dd HH：mm");
                                FileStream file = new FileStream(BaseConfig.SavePath + "\\" + timeForName + "记录.txt", FileMode.Create, FileAccess.ReadWrite);
                                StreamWriter stingForWrite = new StreamWriter(file); // 创建写入流

                                stmp = Convert.ToString(System.DateTime.Now) + "  姓名:" + CardMsg.Name;
                                stmp = Convert.ToString(System.DateTime.Now) + "  性别:" + CardMsg.Sex;
                                stmp = Convert.ToString(System.DateTime.Now) + "  民族:" + CardMsg.Nation;
                                stmp = Convert.ToString(System.DateTime.Now) + "  出生日期:" + CardMsg.Born;
                                stmp = Convert.ToString(System.DateTime.Now) + "  地址:" + CardMsg.Address;
                                stmp = Convert.ToString(System.DateTime.Now) + "  身份证号:" + CardMsg.IDCardNo;
                                stmp = Convert.ToString(System.DateTime.Now) + "  发证机关:" + CardMsg.GrantDept;
                                stmp = Convert.ToString(System.DateTime.Now) + "  有效期开始:" + CardMsg.UserLifeBegin;
                                stmp = Convert.ToString(System.DateTime.Now) + "  有效期结束:" + CardMsg.UserLifeEnd;

                                stingForWrite.WriteLine("bob hu");
                                stingForWrite.Close();
                            }
                        }
                        else
                        {
                            stmp = Convert.ToString(System.DateTime.Now) + "  读取身份证信息错误";
                            MessageBox.Show(stmp);
                        }
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



                WarningWindow win = new WarningWindow();
            // if (StaffService.CheckStaffById(ID.Text))
            // {
            //StaffService a =new StaffService();
            //if (a.CheckStaffById("sds"))
            //{ }

            if (BaseConfig.AutoCheckin)
            {
                msgBlock.Text = "ACCEPT";
            }
            else
            {
                msgBlock.Text = "ERROR";
                win.ShowDialog();
            }



        }
    }
}