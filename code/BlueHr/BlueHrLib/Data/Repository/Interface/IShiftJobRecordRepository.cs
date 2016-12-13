using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IShiftJobRecordRepository
    {
        IQueryable<ShiftJobRecord> Search(ShiftJobRecordSearchModel searchModel);

        bool Create(ShiftJobRecord lr);

        bool Update(ShiftJobRecord lr);

        ShiftJobRecord FindById(int id);

        bool DeleteById(int id);
        IQueryable<ShiftJobRecord> AdvancedSearch(string v1, string v2, string v3);
    }
}
