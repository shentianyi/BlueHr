using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IRewardsAndPenaltiesRepository
    {
        IQueryable<RewardsAndPenalties> Search(RewardsAndPenaltiesSearchModel searchModel);

        bool Create(RewardsAndPenalties certf);

        RewardsAndPenalties FindById(int id);

        bool Update(RewardsAndPenalties certf);

        bool DeleteById(int id);

        List<RewardsAndPenalties> GetAll();
        List<RewardsAndPenalties> GetAllTableName();
    }
}
