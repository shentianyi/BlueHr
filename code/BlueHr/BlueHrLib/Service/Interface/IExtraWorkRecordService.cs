using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{ 
    public interface IExtraWorkRecordService
    {
        IQueryable<ExtraWorkRecord> Search(ExtraWorkRecordSearchModel searchModel);

        ExtraWorkRecord FindById(int id);

        bool Create(ExtraWorkRecord parModel);

        bool Update(ExtraWorkRecord parModel);

        bool DeleteById(int id);

        ExtraWorkRecordInfoModel GetExtraWorkRecordInfo(ExtraWorkRecordSearchModel searchModel);

        bool Creates(List<ExtraWorkRecord> records);

        ExtraWorkRecordView FindViewByStaffNrAndDate(string staffNr, DateTime date);

        ExtraWorkRecord FindByStaffNrAndDete(string staffNr, DateTime date);

        bool ApprovalTheRecord(ExtraWorkRecordApproval extralApproval);
    }
}
