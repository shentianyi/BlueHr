using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{

    public class ExtraWorkRecordExcelModel : BaseExcelModel
    {
        public static List<string> Headers = new List<string>() { "日期", "工号", "加班类别", "加班时长", "时间单位", "加班原因", };

        /// <summary>
        /// 日期字符串
        /// </summary>
        public string OtTimeStr { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? OtTime
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.OtTimeStr).Date;
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

        public string ExtraWorkTypeStr { get; set; }

        public int ExtraWorkTypeId { get; set; }

        /// <summary>
        /// 时间单位字符串
        /// </summary>
        public string DurationTypeStr { get; set; }

        /// <summary>
        /// 时间单位
        /// </summary>
        public int DurationType { get; set; }

        /// <summary>
        /// 加班原因
        /// </summary>
        public string OtReason { get; set; }

        /// <summary>
        /// 加班小时
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

            if (string.IsNullOrEmpty(this.OtTimeStr))
            {
                msg.Contents.Add("日期不可空");
            }
            else
            {
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(this.OtTimeStr, out dt))
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

            if (string.IsNullOrEmpty(this.ExtraWorkTypeStr))
            {
                msg.Contents.Add("加班类别不可空");
            }
            else
            {
                IExtraWorkTypeService si = new ExtraWorkTypeService(dbString);
                List<ExtraWorkType> absTs = si.All();

                bool hasExists = absTs.Where(p => p.name.Equals(this.ExtraWorkTypeStr)).Count() > 0;

                if (!hasExists)
                {
                    msg.Contents.Add("加班类别不存在");
                }
                else
                {
                    ExtraWorkType abs = absTs.Where(p => p.name.Equals(this.ExtraWorkTypeStr)).FirstOrDefault();
                    this.ExtraWorkTypeId = abs.id;
                }
            }

            if (string.IsNullOrEmpty(this.DurationTypeStr))
            {
                msg.Contents.Add("时间单位不可空");
            }
            else
            {
                bool isVal = DurationTypeStr != "Hour" || DurationTypeStr != "Day";

                if (!isVal)
                {
                    msg.Contents.Add("时间单位不存在");
                }
                else
                {
                    if (DurationTypeStr == "天" || DurationTypeStr == "Day")
                    {
                        DurationType = (int)BlueHrLib.Data.Enum.DurationType.Day;
                    }

                    if (DurationTypeStr == "小时" || DurationTypeStr == "Hour")
                    {
                        DurationType = (int)BlueHrLib.Data.Enum.DurationType.Hour;
                    }
                }
            }

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


        public static List<ExtraWorkRecord> Convert(List<ExtraWorkRecordExcelModel> models)
        {
            List<ExtraWorkRecord> records = new List<ExtraWorkRecord>();

            models.ForEach(p =>
            {
                bool hasValid = p.ValidateMessage != null && p.ValidateMessage.Success;

                if (hasValid)
                {
                    ExtraWorkRecord abr = new ExtraWorkRecord();
                    abr.extraWorkTypeId = p.ExtraWorkTypeId;
                    abr.staffNr = p.StaffNr;
                    abr.duration = double.Parse(p.Duration);
                    abr.durationType = p.DurationType;
                    abr.otReason = p.OtReason;
                    abr.otTime = p.OtTime;
                    records.Add(abr);
                }
            });

            return records;
        }
    }
}

