using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface ILeaveRecordRepository
    {
        IQueryable<LeaveRecord> Search(LeaveRecordSearchModel searchModel);

        bool Create(LeaveRecord lr);

        bool Update(LeaveRecord lr);

        LeaveRecord FindById(int id);

        bool DeleteById(int id);
        IQueryable<LeaveRecord> AdvancedSearch(string v1, string v2, string v3);
    }
}
