using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LibFileHelper = BlueHrLib.Helper.FileHelper;

namespace BlueHrWeb.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// 保存到TmpFile文件夹
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string SaveAsTmp(HttpPostedFileBase file)
        {
            string fileName = LibFileHelper.CreateFullTmpFilePath(Path.GetFileName(file.FileName));
            file.SaveAs(fileName);
            return fileName;
        }


        public static string SaveUploadImage(HttpPostedFileBase file)
        {
            string filePath = LibFileHelper.CreateFullUPloadImageFilePath(Path.GetFileName(file.FileName));
            file.SaveAs(filePath);

            string fileName = filePath.Split('\\')[filePath.Split('\\').Length - 1];

            return fileName;
        }

    }
}