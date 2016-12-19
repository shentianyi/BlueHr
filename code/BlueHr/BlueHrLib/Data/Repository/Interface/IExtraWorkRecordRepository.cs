using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IExtraWorkRecordRepository
    {
        IQueryable<ExtraWorkRecord> Search(ExtraWorkRecordSearchModel searchModel);

        bool Create(ExtraWorkRecord parModel);

        ExtraWorkRecord FindById(int id);

        bool Update(ExtraWorkRecord parModel);

        bool DeleteById(int id);

        bool ApprovalTheRecord(ExtraWorkRecordApproval extralApproval);
        List<ExtraWorkRecordView> GetAllTableName();
        IQueryable<ExtraWorkRecordView> ExtraWorkViewSearch(ExtraWorkRecordSearchModel searchModel);
        IQueryable<ExtraWorkRecordView> AdvancedSearch(string v1, string v2, string v3);
    }
}