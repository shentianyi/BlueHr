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
    public class RecruitService : ServiceBase, IRecruitService
    {
        public RecruitService(string dbString) : base(dbString) { }

        public bool Create(Recruit certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.Create(certf);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.DeleteById(id);
        }

        public Recruit FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.FindById(id);
        }

        public RecruitInfoModel GetRecruitInfo(RecruitSearchModel searchModel)
        {
            RecruitInfoModel info = new RecruitInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            IQueryable<Recruit> certfs = certfRep.Search(searchModel);

            info.RecruitCount = dc.Context.GetTable<Recruit>().Where(c => c.id.Equals(certfs.Count() > 0 ? certfs.First().id : -1)).Count();

            return info;
        }

        public IQueryable<Recruit> Search(RecruitSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.Search(searchModel);
        }

        public bool Update(Recruit certf)
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.Update(certf);
        }

        public List<Recruit> GetAll()
        {
            DataContext dc = new DataContext(this.DbString);
            IRecruitRepository certfRep = new RecruitRepository(dc);
            return certfRep.GetAll();
        }
    }
}
