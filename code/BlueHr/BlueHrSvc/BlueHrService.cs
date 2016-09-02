using BlueHrLib.MQTask;
using BlueHrSvc.Properties;
using Brilliantech.Framwork.Utils.LogUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrSvc
{
    public partial class BlueHrService : ServiceBase
    {
        System.Timers.Timer timer;
        public BlueHrService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogUtil.Logger.Info("服务启动中....");
            if (!MessageQueue.Exists(Settings.Default.queue))
            {
                LogUtil.Logger.Info(string.Format("消息队列 {0} 不存在，请先建立!", Settings.Default.db));
                this.Stop();
                return;
            }
            timer = new System.Timers.Timer();
            timer.Interval = Settings.Default.interval;
            timer.Enabled = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            LogUtil.Logger.Info("服务启动【成功】");
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

        protected override void OnStop()
        {

            LogUtil.Logger.Info("服务停止中....");

            if (timer != null)
            {
                timer.Stop();
                timer.Enabled = false;
            }

            LogUtil.Logger.Info("服务停止【成功】");
        }
    }
}
