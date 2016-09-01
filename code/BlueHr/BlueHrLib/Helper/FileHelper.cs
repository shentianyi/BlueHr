using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper
{
    public class FileHelper
    {
        public const string TMP_FILE_DIR = "TmpFile";

        /// <summary>
        /// 创建临时文件路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateFullTmpFilePath(string fileName,bool isErrorFile=false)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TMP_FILE_DIR);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!isErrorFile)
            {
                return Path.Combine(dir,
                   DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + (fileName.Length > 70 ? Guid.NewGuid().ToString() + Path.GetExtension(fileName) : fileName));
            }
            else
            {
                return Path.Combine(dir,
                  DateTime.Now.ToString("yyyyMMddHHmmss") + "_错误文件_" + Guid.NewGuid().ToString() + Path.GetExtension(fileName));
            
            }
        }

        /// <summary>
        /// 获取可供下载的临时文件路径，
        /// 如网站中的，<a href="/TmpFile/xxx.docx">下载</a>
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetDownloadTmpFilePath(string fullPath)
        {
            return Path.Combine("/",TMP_FILE_DIR, Path.GetFileName(fullPath));
        }
    }
}
