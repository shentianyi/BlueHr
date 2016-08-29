using Brilliantech.Framwork.Utils.ConfigUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace BlueHrClient.Config
{
    public class BaseConfig
    {
        private static ConfigUtil config;
        private static bool autoCheckin;
        public static bool saveNotes;
        private static string savePath;
        public static int timeForMsg;


        static BaseConfig()
        {

            config = new ConfigUtil("BASE", "Config/base.ini");

            autoCheckin =bool.Parse( config.Get("AutoCheckin"));
            saveNotes = bool.Parse(config.Get("SaveNotes"));
            savePath = config.Get("SavePath");
            timeForMsg = int.Parse(config.Get("TimeForMsg"));
        }

        public static bool AutoCheckin
        {
            get
            {
                return autoCheckin;
            }

            set
            {
                autoCheckin = value;
                config.Set("AutoCheckin", value);
                config.Save();
            }
        }
        public static bool SaveNotes
        {
            get
            {
                return saveNotes;
            }

            set
            {
                saveNotes = value;
                config.Set("SaveNotes", value);
                config.Save();
            }
        }
        public static string SavePath
        {
            get
            {
                return savePath;
            }

            set
            {
                savePath = value;
                config.Set("SavePath", value);
                config.Save();
            }
        }
        public static int TimeForMsg
        {
            get
            {
                return timeForMsg;
            }
            set
            {
                timeForMsg = value;
                config.Set("TimeForMsg", value);
                config.Save();
            }
        }
    }
   

}

