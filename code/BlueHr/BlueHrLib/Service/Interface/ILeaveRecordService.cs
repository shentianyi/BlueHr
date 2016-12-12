using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface ILeaveRecordService
    {
        IQueryable<LeaveRecord> Search(LeaveRecordSearchModel searchModel);

        LeaveRecordInfoModel GetLeaveRecordInfo(LeaveRecordSearchModel searchModel);

        LeaveRecord FindById(int id);

        bool Create(LeaveRecord leaveRecord);

        bool Update(LeaveRecord title);

        bool DeleteById(int id);

    }
}
