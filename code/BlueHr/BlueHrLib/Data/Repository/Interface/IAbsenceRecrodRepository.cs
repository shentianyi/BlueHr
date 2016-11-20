using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IAbsenceRecrodRepository
    {
        IQueryable<AbsenceRecrod> Search(AbsenceRecrodSearchModel searchModel);
        IQueryable<AbsenceRecordView> SearchDetail(AbsenceRecrodSearchModel searchModel);

        bool Create(AbsenceRecrod absenceRecord);

        AbsenceRecrod FindById(int id);

        bool Update(AbsenceRecrod absenceRecord);

        bool DeleteById(int id);

        List<AbsenceRecrod> FindByAbsenceType(int id);

        List<AbsenceRecrod> GetAll();

        bool ApprovalTheRecord(AbsenceRecordApproval absRecordApproval);
    }
}