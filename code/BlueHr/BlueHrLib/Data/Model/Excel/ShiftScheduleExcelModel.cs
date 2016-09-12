using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Data.Model.Excel
{
    public class ShiftScheduleExcelModel : BaseExcelModel
    {
        public static List<string> Headers = new List<string>() {   "人员编号", "姓名", "日期", "班次代码" };
         

        /// <summary>
        /// 人员编号
        /// </summary>
        public string StaffNr { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 日期字符串
        /// </summary>
        public string ScheduleAtDateStr { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? ScheduleAt
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.ScheduleAtDateStr).Date;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 班次号
        /// </summary>
        public string ShiftCode { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public Shift Shift { get; set; }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

         

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
            }

            if (string.IsNullOrEmpty(this.ScheduleAtDateStr))    
            {
                msg.Contents.Add("日期不可空");
            }
            else
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(this.ScheduleAtDateStr, out dt))
                {
                    //this.RecordAtDate = dt;
                }
                else
                {
                    msg.Contents.Add("日期格式错误");
                }
            }


           

            if (string.IsNullOrEmpty(this.ShiftCode))
            {
                msg.Contents.Add("班次号不可空");
            }
            else
            {
                IShiftService ss = new ShiftService(dbString);
                Shift r = ss.FindByCode(this.ShiftCode);
                if (r == null)
                { 
                    msg.Contents.Add("班次号不存在");
                }
                else
                {
                    this.Shift = r;
                }
            }

            if (msg.Contents.Count == 0)
            {
                if (this.ScheduleAt.HasValue)
                {
                    IShiftScheduleService ss = new ShiftSheduleService(dbString);
                    
                    if (ss.IsDup(new ShiftSchedule() { id=0, scheduleAt=this.ScheduleAt.Value, shiftId=this.Shift.id, staffNr=this.StaffNr}))  
                    {
                        msg.Contents.Add("排班记录已存在，不可重复导入");
                    }
                }
            }

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }

        
        public static List<ShiftSchedule> Convert(List<ShiftScheduleExcelModel> models)
        {
            List<ShiftSchedule> records = new List<ShiftSchedule>();
            foreach (var m in models)
            {
                if (m.ValidateMessage != null && m.ValidateMessage.Success)
                {
                    records.Add(new ShiftSchedule()
                    {
                        staffNr = m.StaffNr,
                        scheduleAt = m.ScheduleAt.Value,
                        shiftId = m.Shift.id
                    });
                }
            }
            return records;
        }
    }
}
