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
        public static bool sound;
        private static string savePath;
        private static string savePathPhoto;
        public static int timeForMsg;
        public static int nRet;
        public static int nPort;


        static BaseConfig()
        {

            config = new ConfigUtil("BASE", "Config/base.ini");

            autoCheckin =bool.Parse( config.Get("AutoCheckin"));
            saveNotes = bool.Parse(config.Get("SaveNotes"));
            sound = bool.Parse(config.Get("Sound"));
            savePath = config.Get("SavePath");
            savePathPhoto = config.Get("SavePathPhoto");
            timeForMsg = int.Parse(config.Get("TimeForMsg"));
            nRet = int.Parse(config.Get("NRet"));
            nPort = int.Parse(config.Get("NPort"));
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
        public static bool Sound
        {
            get
            {
                return sound;
            }

            set
            {
                sound = value;
                config.Set("Sound", value);
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
        public static string SavePathPhoto
        {
            get
            {
                return savePathPhoto;
            }

            set
            {
                savePathPhoto = value;
                config.Set("SavePathPhoto", value);
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

        public static int NRet
        {
            get
            {
                return nRet;
            }
            set
            {
                nRet = value;
                config.Set("NRet", value);
                config.Save();
            }
        }

        public static int NPort
        {
            get
            {
                return nPort;
            }
            set
            {
                nPort = value;
                config.Set("NPort", value);
                config.Save();
            }
        }
    }
   

}

