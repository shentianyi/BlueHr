﻿using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{ 
   public interface IAbsenceRecordService
    {
        IQueryable<AbsenceRecrod> Search(AbsenceRecrodSearchModel searchModel);
        IQueryable<AbsenceRecordView> SearchDetail(AbsenceRecrodSearchModel searchModel);

        AbsenceRecrod FindById(int id);

        bool Create(AbsenceRecrod title);

        bool Update(AbsenceRecrod title);

        bool DeleteById(int id);

        AbsenceRecrodInfoModel GetAbsenceRecrodInfo(AbsenceRecrodSearchModel searchModel);

        List<AbsenceRecrod> FindByAbsenceType(int id);

        bool Creates(List<AbsenceRecrod> records);

        List<AbsenceRecrod> GetAll();

        AbsenceRecordView FindViewByStaffNrAndDate(string staffNr, DateTime date);

        bool ApprovalTheRecord(AbsenceRecordApproval absRecordApproval);

        List<AbsenceRecrod> GetAllTableName();
        int countStaffAbsence(DateTime StartTime, DateTime EndTime);
        int countStaffLate(DateTime StartTime, DateTime EndTime);
        int countStaffLeave(DateTime StartTime, DateTime EndTime);
    }
}
