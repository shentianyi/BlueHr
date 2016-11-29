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

    public class SysAuthorizationService : ServiceBase, ISysAuthorizationService
    {
        private ISysAuthorizationRepository rep;

        public SysAuthorizationService(string dbString) : base(dbString)
        {
            rep = new SysAuthorizationRepository(this.Context);
        }

        public IQueryable<SysAuthorization> Search(SysAuthorizationSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(SysAuthorization model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public SysAuthorization FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(SysAuthorization model)
        {
            return rep.Update(model);
        }

        public SysAuthorizationInfoModel GetSysAuthorizationInfo(SysAuthorizationSearchModel searchModel)
        {
            SysAuthorizationInfoModel info = new SysAuthorizationInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ISysAuthorizationRepository rep = new SysAuthorizationRepository(dc);
            IQueryable<SysAuthorization> results = rep.Search(searchModel);

            info.SysAuthorizationCount = dc.Context.GetTable<SysAuthorization>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public bool Creates(List<SysAuthorization> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<SysAuthorization>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public List<SysAuthorization> GetAll()
        {
            return rep.GetAll();
        }

        public List<SysAuthorization> GetSysAuthByRoleId(string roleId)
        {
            return rep.GetSysAuthByRoleId(roleId);
        }

        public List<SysAuthorization> GetAllTableName()
        {
            return rep.GetAllTableName();
        }

        public IQueryable<SysAuthorization> AdvancedSearch(string allTableName, string searchConditions, string searchValueFirst, string searchValueSecond)
        {
            return rep.AdvancedSearch(allTableName, searchConditions, searchValueFirst, searchValueSecond);
        }
    }
}