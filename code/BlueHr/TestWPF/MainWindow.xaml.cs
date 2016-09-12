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
using BlueHrLib.MQTask;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using Brilliantech.Framwork.Utils.LogUtil;
using TestWPF.Properties;
using BlueHrLib.MQTask.Parameter;
using BlueHrLib.Helper;
using Quartz;
using BlueHrLib.MQTask.Job;
using Quartz.Impl;
using System.Messaging;

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

        System.Timers.Timer timer;
        private static IScheduler scheduler;
        public static IScheduler Scheduler { get { return scheduler; } set { scheduler = value; } }

        private void runSvcBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogUtil.Logger.Info("服务启动中....");
                if (!MessageQueue.Exists(Settings.Default.queue))
                {
                    LogUtil.Logger.Info(string.Format("消息队列 {0} 不存在，请先建立!", Settings.Default.queue));
                    
                    return;
                }

                Load();

                LogUtil.Logger.Info("服务启动【成功】");
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("服务启动【失败】", ex);
                
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                LogUtil.Logger.Info("获取任务信息....");
                new TaskDispatcher(Settings.Default.db, Settings.Default.queue).FetchMQMessage();
                LogUtil.Logger.Info("任务运行结束");
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("服务运行时出错", ex);
            }

            timer.Start();
        }

        private void calTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDateTime =DateTime.Parse( startDate.Text +" "+ timeTB.Text);

            DateTime endDateTime = DateTime.Parse(endDate.Text + " " + timeTB.Text);

            TaskDispatcher dtt = new TaskDispatcher(Settings.Default.queue);

            for (DateTime dt = startDateTime; dt <= endDateTime; dt=dt.AddDays(1))
            {
                dtt.SendCalculateAttMessage(dt.Date.AddHours(23));
            }

            MessageBox.Show("OK");
        }

        private void restartSvcBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskDispatcher dtt = new TaskDispatcher(Settings.Default.queue);
            dtt.SendRestartSvcMessage();
        }


        /// <summary>
        /// 加载配置
        /// </summary>
        private void Load(bool startTimer = true)
        {
            LogUtil.Logger.Info("【加载配置】");
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            if (Scheduler != null)
            {
                Scheduler.Shutdown();
                Scheduler = null;
            }

            IQuartzJobService jobService = new QuartzJobService(Settings.Default.db);
            List<QuartzJob> toFullJobs = jobService.GetByType(CronJobType.ToFullWarn);
            List<QuartzJob> calAttJobs = jobService.GetByType(CronJobType.CalAtt);

           
         

            // 定义定时任务
            ISchedulerFactory sf = new StdSchedulerFactory();
            Scheduler = sf.GetScheduler();
            ToFullMemberJobTrigger trigger = new ToFullMemberJobTrigger(toFullJobs, Settings.Default.db, Settings.Default.queue);

            for (int i = 0; i < trigger.Triggers.Count; i++)
            {
                Scheduler.ScheduleJob(trigger.TriggerJobs[i], trigger.Triggers[i]);
            }

            CalAttJobTrigger trigger1 = new CalAttJobTrigger(calAttJobs, Settings.Default.db, Settings.Default.queue);

            for (int i = 0; i < trigger1.Triggers.Count; i++)
            {
                Scheduler.ScheduleJob(trigger1.TriggerJobs[i], trigger1.Triggers[i]);
            }

            Scheduler.Start();

            // 循环消息
            timer = new System.Timers.Timer();
            timer.Interval = Settings.Default.interval;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            if (startTimer)
            {
                timer.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtil.Logger.Info("服务停止中....");
                if (Scheduler != null)
                {
                    Scheduler.Shutdown();
                }
                if (timer != null)
                {
                    timer.Stop();
                    timer.Enabled = false;
                }

                LogUtil.Logger.Info("服务停止【成功】");
            }
            catch (Exception ex)
            {
                LogUtil.Logger.Error("服务停止【失败】", ex);
            }
        }
    }
}
