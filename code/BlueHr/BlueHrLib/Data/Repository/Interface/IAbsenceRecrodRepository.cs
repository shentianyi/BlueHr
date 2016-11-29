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
        List<AbsenceRecrod> GetAllTableName();
        int countStaffAbsence(DateTime d1, DateTime d2);
        int countStaffLate(DateTime d1, DateTime d2);
        int countStaffLeave(DateTime d1, DateTime d2);
    }
}