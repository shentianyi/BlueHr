using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IRewardsAndPenaltyService
    {
        IQueryable<RewardsAndPenalty> Search(RewardsAndPenaltySearchModel searchModel);

        RewardsAndPenaltyInfoModel GetRewardsAndPenaltyInfo(RewardsAndPenaltySearchModel searchModel);

        RewardsAndPenalty FindById(int id);

        bool Create(RewardsAndPenalty title);

        bool Update(RewardsAndPenalty title);

        bool DeleteById(int id);

        List<RewardsAndPenalty> GetAll();
    }
}
