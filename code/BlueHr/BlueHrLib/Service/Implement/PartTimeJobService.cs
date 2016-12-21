using BlueHrLib.Data;
using BlueHrLib.Data.Message;
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
    public class PartTimeJobService : ServiceBase, IPartTimeJobService
    {
        private IPartTimeJobRepository rep;

        public PartTimeJobService(string dbString) : base(dbString)
        {
            rep = new PartTimeJobRepository(this.Context);
        }

        public IQueryable<PartTimeJob> Search(PartTimeJobSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(PartTimeJob model)
        {
            bool isSucceed = false;
            try
            {
                isSucceed = rep.Create(model);
            }
            catch (Exception)
            {
            }

            return isSucceed;
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public PartTimeJob FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(PartTimeJob model)
        {
            return rep.Update(model);
        }

        public PartTimeJobInfoModel GetPartTimeJobInfo(PartTimeJobSearchModel searchModel)
        {
            PartTimeJobInfoModel info = new PartTimeJobInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IPartTimeJobRepository rep = new PartTimeJobRepository(dc);
            IQueryable<PartTimeJob> results = rep.Search(searchModel);

            info.partTimeJobCount = dc.Context.GetTable<PartTimeJob>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public IQueryable<PartTimeJob> AdvancedSearch(string v1, string v2, string v3)
        {
            return rep.AdvancedSearch(v1, v2, v3);
        }

        public List<PartTimeJob> FindBystaffNrPartTimeJob(string staffNr)
        {
            return rep.FindBystaffNrPartTimeJob(staffNr);
        }
    }
}