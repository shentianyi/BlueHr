using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class AttendanceRecordDetailExcelModel:BaseExcelModel
    {
        public static List<string> Headers = new List<string>() { "机构名称", "人员编号", "姓名", "刷卡日期", "刷卡时间", "设备编号","备注" };

          
        /// <summary>
        /// 机构名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string StaffNr { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 刷卡日期
        /// </summary>
        public string RecordAtDateStr { get; set; }
     //   public DateTime RecordAtDate { get; set; }

        /// <summary>
        /// 刷卡时间
        /// </summary>
        public string RecordAtTimeStr { get; set; }
       // public TimeSpan RecordAtTime { get; set; }

        public DateTime? RecordAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.RecordAtDateStr).Add(TimeSpan.Parse(this.RecordAtTimeStr));
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

            if ( string.IsNullOrEmpty(this.DepartmentName))
            {
                msg.Contents.Add("机构名称不可空");
            }

            if (string.IsNullOrEmpty(this.StaffNr))
            {
                msg.Contents.Add("人员编号不可空");
            }
            else
            {
                Staff staff = new StaffService(dbString).FindByNr(this.StaffNr);
                if (staff == null)
                {
                    msg.Contents.Add("人员编号不存在");
                }
                else
                {
                    AttendanceRecordDetail detail = new AttendanceRecordDetailService(dbString).FindDetailByStaffAndRecordAt(this.StaffNr, this.RecordAt.Value);
                    if (detail != null)
                    {
                        msg.Contents.Add("打卡记录已存在，不可重复导入");
                    }
                }
            }

            if (string.IsNullOrEmpty(this.RecordAtDateStr))
            {
                msg.Contents.Add("刷卡日期不可空");
            }
            else
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(this.RecordAtDateStr, out dt))
                {
                    //this.RecordAtDate = dt;
                }
                else
                {
                    msg.Contents.Add("刷卡日期格式错误");
                }
            }


            if (string.IsNullOrEmpty(this.RecordAtTimeStr))
            {
                msg.Contents.Add("刷卡时间不可空");
            }
            else
            {
                TimeSpan ts = DateTime.Now.TimeOfDay;
                if (TimeSpan.TryParse(this.RecordAtTimeStr, out ts))
                {
                    //   this.RecordAtTime = ts;
                }
                else
                {
                    msg.Contents.Add("刷卡时间格式错误");
                }
            }

            if (string.IsNullOrEmpty(this.Device))
            {
                msg.Contents.Add("设备编号不可空");
            }

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }

        
        public static List<AttendanceRecordDetail> Convert(List<AttendanceRecordDetailExcelModel> models)
        {
            List<AttendanceRecordDetail> records = new List<AttendanceRecordDetail>();
            foreach (var m in models)
            {
                if (m.ValidateMessage != null && m.ValidateMessage.Success)
                {
                    records.Add(new AttendanceRecordDetail()
                    {
                        staffNr = m.StaffNr,
                        recordAt = m.RecordAt.Value,
                        createdAt = DateTime.Now,
                        device = m.Device,
                        soureType = SOURCE_TYPE
                    });
                }
            }
            return records;
        }
    }
}
