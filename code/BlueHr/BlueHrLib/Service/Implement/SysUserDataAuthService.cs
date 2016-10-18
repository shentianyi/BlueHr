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

    public class SysUserDataAuthService : ServiceBase, ISysUserDataAuthService
    {
        private ISysUserDataAuthRepository rep;

        public SysUserDataAuthService(string dbString) : base(dbString)
        {
            rep = new SysUserDataAuthRepository(this.Context);
        }

        public IQueryable<SysUserDataAuth> Search(SysUserDataAuthSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(SysUserDataAuth model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public SysUserDataAuth FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(SysUserDataAuth model)
        {
            return rep.Update(model);
        }

        public SysUserDataAuthInfoModel GetSysRoleInfo(SysUserDataAuthSearchModel searchModel)
        {
            SysUserDataAuthInfoModel info = new SysUserDataAuthInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ISysUserDataAuthRepository rep = new SysUserDataAuthRepository(dc);
            IQueryable<SysUserDataAuth> results = rep.Search(searchModel);

            info.SysUserDataAuthCount = dc.Context.GetTable<SysUserDataAuth>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public bool Creates(List<SysUserDataAuth> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<SysUserDataAuth>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public List<SysUserDataAuth> GetAll()
        {
            return rep.GetAll();
        }
    }
}