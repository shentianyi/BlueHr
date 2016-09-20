using BlueHrLib.Data.Message;
using BlueHrLib.Properties;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class WorkAndRestExcelModel : BaseExcelModel
    {

        public static List<string> Headers = new List<string>() { "日期", "类型", "备注"};

        /// <summary>
        /// 日期
        /// </summary>
        public string DateAtStr { get; set; }
        public DateTime? DateAt
        {
            get {
                try
                {
                    return DateTime.Parse(this.DateAtStr);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string DateTypeStr { get; set; }

        public int? DateType
        {
            get
            {
                if (DateTypeStr == "法定假日"|| DateTypeStr == "调休日"||DateTypeStr=="节假日")
                {
                    return 300;
                }
                else if (DateTypeStr == "公休日"|| DateTypeStr=="双休日")
                {
                    return 200;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

            if (string.IsNullOrEmpty(this.DateAtStr))
            {
                msg.Contents.Add("日期不可空");
            }

            if (!DateAt.HasValue)
            {
                msg.Contents.Add("日期格式不正确");
            }

            IWorkAndRestService wrs = new WorkAndRestService(dbString);
            bool HasExist = wrs.HasDateAtExist(DateAt);

            if (!HasExist)
            {
                msg.Contents.Add("日期已经存在, 请进行编辑");
            }

            if (string.IsNullOrEmpty(this.DateTypeStr))
            {
                msg.Contents.Add("类型不可空");
            }

            if (!DateType.HasValue)
            {
                msg.Contents.Add("类型不正确，只能为（法定假日/调休日/节假日）或者（公休日/双休日）");
            }

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }

        public static List<WorkAndRest> Convert(List<WorkAndRestExcelModel> models)
        {
            List<WorkAndRest> records = new List<WorkAndRest>();
            foreach (var m in models)
            {
                if (m.ValidateMessage != null && m.ValidateMessage.Success)
                {
                    records.Add(new WorkAndRest()
                    {
                        dateAt = m.DateAt,
                        dateType = m.DateType,
                        remark = m.Remark
                    });
                }
            }
            return records;
        }
    }
}
