using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IRewardsAndPenaltyRepository
    {
        IQueryable<RewardsAndPenalty> Search(RewardsAndPenaltySearchModel searchModel);

        bool Create(RewardsAndPenalty certf);

        RewardsAndPenalty FindById(int id);

        bool Update(RewardsAndPenalty certf);

        bool DeleteById(int id);

        List<RewardsAndPenalty> GetAll();
        List<RewardsAndPenalty> GetAllTableName();
    }
}
