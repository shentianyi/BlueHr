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

        
       
    }
}
