using BlueHrLib.CusException;
using BlueHrLib.Data;
using BlueHrLib.Data.Enum;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Data.Repository.Implement;

namespace BlueHrLib.Service.Implement
{
    public class AttendanceRecordDetailService : ServiceBase, IAttendanceRecordDetailService
    {
        public AttendanceRecordDetailService(string dbString) : base(dbString) { }

    
        /// <summary>
        /// 搜索详细考勤信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordDetail> SearchDetail(AttendanceRecordDetailSearchModel searchModel)
        {
            IAttendanceRecordDetailRepository rep = new AttendanceRecordDetailRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }
        
        /// <summary>
        /// 搜索详细考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordDetailView> SearchDetailView(AttendanceRecordDetailSearchModel searchModel)
        {
            IAttendanceRecordDetailViewRepository rep = new AttendanceRecordDetailViewRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }

    

        /// <summary>
        /// 批量创建详细考勤数据
        /// </summary>
        /// <param name="records"></param>
        public void CreateDetails(List<AttendanceRecordDetail> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<AttendanceRecordDetail>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
        }

        /// <summary>
        /// 根据员工号和考勤时间查询详细记录
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="recordAt"></param>
        /// <returns></returns>
        public AttendanceRecordDetail FindDetailByStaffAndRecordAt(string nr, DateTime recordAt)
        {
            DataContext dc = new DataContext(this.DbString);

            return dc.Context.GetTable<AttendanceRecordDetail>().FirstOrDefault(s => s.staffNr.Equals(nr) && s.recordAt.Equals(recordAt));
        }

        /// <summary>
        /// 根据员工号和日期获取员工的详细打卡记录视图
        /// 重点是，根据日期来判断排班，然后通过排班来查找所有的记录
        /// 如果排班时间开始都是在当前日期的排班，但是计算时要用下面废弃的！
        /// **如果排班的结束时间的HH:mm小于日期的HH:mm，并且结束时间的天等于（或加1等于）日期的天
        /// schedule视图里的fullEnd时间已经是根据排班类型计算过的了 -废弃，Charlot 2016.09.02**
        /// </summary>
        /// <param name="nr"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public List<AttendanceRecordDetailView> GetDetailsViewByStaffAndDate(string nr, DateTime datetime)
        {
            DataContext dc = new DataContext(this.DbString);
            List<AttendanceRecordDetailView> records = new List<AttendanceRecordDetailView>();

            /// 获取所有排班
            List<ShiftScheduleView> shifts= dc.Context.GetTable<ShiftScheduleView>().Where(s =>s.staffNr.Equals(nr) && s.fullStartAt.Value.Date.Equals(datetime.Date) && s.fullEndAt.Value<datetime).ToList();
            /// 系统配置
            SystemSetting setting = dc.Context.GetTable<SystemSetting>().FirstOrDefault();
            if (setting == null)
                throw new SystemSettingNotSetException();

            foreach(ShiftScheduleView s in shifts)
            {
                DateTime sq = s.fullStartAt.Value.AddHours(0-setting.validAttendanceRecordTime.Value);
                DateTime eq = s.fullEndAt.Value.AddHours(setting.validAttendanceRecordTime.Value);

                List<AttendanceRecordDetailView> shiftAttendRecords = dc.Context.GetTable<AttendanceRecordDetailView>().Where(ss=>ss.recordAt>=sq && ss.recordAt<=eq && ss.staffNr.Equals(nr)).OrderBy(ss=>ss.recordAt).ToList() ;//new List<AttendanceRecordDetailView>();

                records.AddRange(shiftAttendRecords);
            }

            records = records.Distinct().ToList();
            
            return records;
        }
    }
}
