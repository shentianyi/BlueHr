using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class AbsenceRecordExcelModel : BaseExcelModel
    {
        public static List<string> Headers = new List<string>() { "日期", "工号", "姓名", "缺勤类型", "缺勤原因", "缺勤小时", "时间单位" };

        /// <summary>
        /// 日期字符串
        /// </summary>
        public string AbsenceDateStr { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? AbsenceDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.AbsenceDateStr).Date;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 工号
        /// </summary>
        public string StaffNr { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        public string AbsenceTypeStr { get; set; }

        public int AbsenceTypeId { get; set; }

        /// <summary>
        /// 时间单位字符串
        /// </summary>
        public string DurationTypeStr { get; set; }

        /// <summary>
        /// 时间单位
        /// </summary>
        public int DurationType { get; set; }

        /// <summary>
        /// 缺勤原因
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 缺勤小时
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

            if (string.IsNullOrEmpty(this.AbsenceDateStr))
            {
                msg.Contents.Add("日期不可空");
            }
            else
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(this.AbsenceDateStr, out dt))
                {
                    //this.RecordAtDate = dt;
                }
                else
                {
                    msg.Contents.Add("日期格式错误");
                }
            }

            if (string.IsNullOrEmpty(this.StaffNr))
            {
                msg.Contents.Add("工号");
            }
            else
            {
                Staff staff = new StaffService(dbString).FindByNr(this.StaffNr);
                if (staff == null)
                {
                    msg.Contents.Add("人员编号不存在");
                }
            }

            if (string.IsNullOrEmpty(this.AbsenceTypeStr))
            {
                msg.Contents.Add("缺勤类型不可空");
            }
            else
            {
                IAbsenceTypeService si = new AbsenceTypeService(dbString);
                List<AbsenceType> absTs = si.GetAll();

                bool hasExists = absTs.Where(p => p.name.Equals(this.AbsenceTypeStr)).Count() > 0;

                if (!hasExists)
                {
                    msg.Contents.Add("缺勤类型不存在");
                }
                else
                {
                    AbsenceType abs = absTs.Where(p => p.name.Equals(this.AbsenceTypeStr)).FirstOrDefault();
                    this.AbsenceTypeId = abs.id;
                }
            }

            //if (string.IsNullOrEmpty(this.DurationTypeStr))
            //{
            //    msg.Contents.Add("时间单位不可空");
            //}
            //else
            //{
            //    bool isVal = DurationTypeStr != "Hour" || DurationTypeStr != "Day";

            //    if (!isVal)
            //    {
            //        msg.Contents.Add("时间单位不存在");
            //    }
            //    else
            //    {
            //        if (DurationTypeStr == "天" || DurationTypeStr == "Day")
            //        {
            //            DurationType = (int)BlueHrLib.Data.Enum.DurationType.Day;
            //        }

            //        if (DurationTypeStr == "小时" || DurationTypeStr == "Hour")
            //        {
            //            DurationType = (int)BlueHrLib.Data.Enum.DurationType.Hour;
            //        }
            //    }
            //}

            //if (string.IsNullOrEmpty(Remark))
            //{
            //    msg.Contents.Add("缺勤原因不可空");
            //}

            if (string.IsNullOrEmpty(this.Duration))
            {
                msg.Contents.Add("缺勤小时不可空");
            }

            //if (msg.Contents.Count == 0)
            //{
            //    if (this.ScheduleAt.HasValue)
            //    {
            //        IShiftScheduleService ss = new ShiftSheduleService(dbString);

            //        if (ss.IsDup(new ShiftSchedule() { id = 0, scheduleAt = this.ScheduleAt.Value, shiftId = this.Shift.id, staffNr = this.StaffNr }))
            //        {
            //            msg.Contents.Add("排班记录已存在，不可重复导入");
            //        }
            //    }
            //}

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }


        public static List<AbsenceRecrod> Convert(List<AbsenceRecordExcelModel> models)
        {
            List<AbsenceRecrod> records = new List<AbsenceRecrod>();

            models.ForEach(p =>
            {
                bool hasValid = p.ValidateMessage != null && p.ValidateMessage.Success;

                if (hasValid)
                {
                    AbsenceRecrod abr = new AbsenceRecrod();
                    abr.absenceTypeId = p.AbsenceTypeId;
                    abr.staffNr = p.StaffNr; 
                    abr.duration = double.Parse(p.Duration);
                    //abr.durationType = p.DurationType;
                    abr.durationType= (int)BlueHrLib.Data.Enum.DurationType.Hour;
                    abr.remark = p.Remark;
                    abr.absenceDate = p.AbsenceDate;
                    records.Add(abr);
                }  
            });

            return records;
        }
    }
}
