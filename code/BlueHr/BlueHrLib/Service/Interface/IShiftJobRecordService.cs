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
    public interface IShiftJobRecordService
    {
        IQueryable<ShiftJobRecord> Search(ShiftJobRecordSearchModel searchModel);

        ShiftJobRecordInfoModel GetShiftJobRecordInfo(ShiftJobRecordSearchModel searchModel);

        ShiftJobRecord FindById(int id);

        bool Create(ShiftJobRecord ShiftJobRecord);

        bool Update(ShiftJobRecord title);

        bool DeleteById(int id);
        IQueryable<ShiftJobRecord> AdvancedSearch(string v1, string v2, string v3);
        //DateTime CalculateTime(int id);
    }
}
