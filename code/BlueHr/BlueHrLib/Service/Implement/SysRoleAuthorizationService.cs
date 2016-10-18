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

    public class SysRoleAuthorizationService : ServiceBase, ISysRoleAuthorizationService
    {
        private ISysRoleAuthorizationRepository rep;

        public SysRoleAuthorizationService(string dbString) : base(dbString)
        {
            rep = new SysRoleAuthorizationRepository(this.Context);
        }

        public IQueryable<SysAuthorization> SearchByRoleAndAuth(SysRoleAuthorizationSearchModel searchModel)
        {
            return rep.SearchByRoleAndAuth(searchModel);
        }

        public bool Create(SysRoleAuthorization model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public SysRoleAuthorization FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(SysRoleAuthorization model)
        {
            return rep.Update(model);
        }

        public SysAuthorizationInfoModel GetSysAuthorizationInfo(SysRoleAuthorizationSearchModel searchModel)
        {
            SysAuthorizationInfoModel info = new SysAuthorizationInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ISysRoleAuthorizationRepository rep = new SysRoleAuthorizationRepository(dc);
            IQueryable<SysAuthorization> results = rep.SearchByRoleAndAuth(searchModel);

            info.SysAuthorizationCount = dc.Context.GetTable<SysAuthorization>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public bool Creates(List<SysRoleAuthorization> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<SysRoleAuthorization>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public List<SysRoleAuthorization> GetAll()
        {
            return rep.GetAll();
        }

        public List<SysRoleAuthorization> GetSysRoleAuthListByRoleName(string roleName)
        {
            return rep.GetSysRoleAuthListByRoleName(roleName);
        }
    }
}
