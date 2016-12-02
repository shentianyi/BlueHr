using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IRecruitService
    {
        IQueryable<Recruit> Search(RecruitSearchModel searchModel);

        RecruitInfoModel GetRecruitInfo(RecruitSearchModel searchModel);

        Recruit FindById(int id);

        bool Create(Recruit title);

        bool Update(Recruit title);

        bool DeleteById(int id);

        List<Recruit> GetAll();
    }
}
