using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IRecruitRepository
    {
        IQueryable<Recruit> Search(RecruitSearchModel searchModel);

        bool Create(Recruit certf);

        Recruit FindById(int id);

        bool Update(Recruit certf);

        bool DeleteById(int id);

        List<Recruit> GetAll();
    }
}
