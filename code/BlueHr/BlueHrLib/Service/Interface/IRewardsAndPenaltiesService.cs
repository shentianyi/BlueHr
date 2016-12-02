using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IRewardsAndPenaltiesService
    {
        IQueryable<RewardsAndPenalties> Search(RewardsAndPenaltiesSearchModel searchModel);

        RewardsAndPenaltiesInfoModel GetRewardsAndPenaltiesInfo(RewardsAndPenaltiesSearchModel searchModel);

        RewardsAndPenalties FindById(int id);

        bool Create(RewardsAndPenalties title);

        bool Update(RewardsAndPenalties title);

        bool DeleteById(int id);

        List<RewardsAndPenalties> GetAll();
    }
}
