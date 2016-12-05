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
    public class RewardsAndPenaltyService : ServiceBase, IRewardsAndPenaltyService
    {
        public RewardsAndPenaltyService(string dbString) : base(dbString) { }

        public bool Create(RewardsAndPenalty certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.Create(certf);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.DeleteById(id);
        }

        public RewardsAndPenalty FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.FindById(id);
        }

        public RewardsAndPenaltyInfoModel GetRewardsAndPenaltyInfo(RewardsAndPenaltySearchModel searchModel)
        {
            RewardsAndPenaltyInfoModel info = new RewardsAndPenaltyInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            IQueryable<RewardsAndPenalty> certfs = certfRep.Search(searchModel);

            info.RewardsAndPenaltyCount = dc.Context.GetTable<RewardsAndPenalty>().Where(c => c.id.Equals(certfs.Count() > 0 ? certfs.First().id : -1)).Count();

            return info;
        }

        public IQueryable<RewardsAndPenalty> Search(RewardsAndPenaltySearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.Search(searchModel);
        }

        public bool Update(RewardsAndPenalty certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.Update(certf);
        }

        public List<RewardsAndPenalty> GetAll()
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.GetAll();
        }

        public List<RewardsAndPenalty> FindBystaffNr(string staffNr)
        {
            DataContext dc = new DataContext(this.DbString);
            IRewardsAndPenaltyRepository certfRep = new RewardsAndPenaltyRepository(dc);
            return certfRep.FindBystaffNr(staffNr);
        }
    }
}
