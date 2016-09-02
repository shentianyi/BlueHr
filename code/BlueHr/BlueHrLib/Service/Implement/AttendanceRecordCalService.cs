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
    public class AttendanceRecordCalService : ServiceBase, IAttendanceRecordCalService
    {
        public AttendanceRecordCalService(string dbString) : base(dbString) { }

       
 
        /// <summary>
        /// 搜索计算考勤信息视图, 包含员工的信息
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public IQueryable<AttendanceRecordCalView> SearchCalView(AttendanceRecordCalSearchModel searchModel)
        {
            IAttendanceRecordCalViewRepository rep = new AttendanceRecordCalViewRepository(new DataContext(this.DbString));
            return rep.Search(searchModel);
        }

        
         

       
    }
}
