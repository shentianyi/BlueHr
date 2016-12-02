using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Implement
{
    public class RewardsAndPenaltiesService : ServiceBase, IRewardsAndPenaltiesService
    {
        public RewardsAndPenaltiesService(string dbString) : base(dbString) { }

        public bool Create(RewardsAndPenalties certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.Create(certf);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.DeleteById(id);
        }

        public RewardsAndPenalties FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.FindById(id);
        }

        public RewardsAndPenaltiesInfoModel GetRewardsAndPenaltiesInfo(RewardsAndPenaltiesSearchModel searchModel)
        {
            RewardsAndPenaltiesInfoModel info = new RewardsAndPenaltiesInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            IQueryable<RewardsAndPenalties> certfs = certfRep.Search(searchModel);

            info.rewardsAndPenaltiesCount = dc.Context.GetTable<RewardsAndPenalties>().Where(c => c.id.Equals(certfs.Count() > 0 ? certfs.First().id : -1)).Count();

            return info;
        }

        public IQueryable<RewardsAndPenalties> Search(RewardsAndPenaltiesSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.Search(searchModel);
        }

        public bool Update(RewardsAndPenalties certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.Update(certf);
        }

        public List<RewardsAndPenalties> GetAll()
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltiesRepository certfRep = new RewardsAndPenaltiesRepository(dc);
            return certfRep.GetAll();
        }
    }
}
