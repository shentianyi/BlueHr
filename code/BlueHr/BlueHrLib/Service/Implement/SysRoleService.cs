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
   
  public class SysRoleService : ServiceBase, ISysRoleService
    {
        private ISysRoleRepository rep;

        public SysRoleService(string dbString) : base(dbString)
        {
            rep = new SysRoleRepository(this.Context);
        }

        public IQueryable<SysRole> Search(SysRoleSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(SysRole model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public SysRole FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(SysRole model)
        {
            return rep.Update(model);
        }

        public SysRoleInfoModel GetSysRoleInfo(SysRoleSearchModel searchModel)
        {
            SysRoleInfoModel info = new SysRoleInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ISysRoleRepository rep = new SysRoleRepository(dc);
            IQueryable<SysRole> results = rep.Search(searchModel);

            info.sysRoleInfoCount = dc.Context.GetTable<SysRole>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public bool Creates(List<SysRole> records)
        {
            DataContext dc = new DataContext(this.DbString);
            dc.Context.GetTable<SysRole>().InsertAllOnSubmit(records);
            dc.Context.SubmitChanges();
            return true;
        }

        public List<SysRole> GetAll()
        {
            return rep.GetAll();
        }
    }
}