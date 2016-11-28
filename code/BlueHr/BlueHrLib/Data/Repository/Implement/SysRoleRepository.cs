using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class SysRoleRepository : RepositoryBase<SysRole>, ISysRoleRepository
    {
        private BlueHrDataContext context;

        public SysRoleRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<SysRole> Search(SysRoleSearchModel searchModel)
        {
            IQueryable<SysRole> q = this.context.SysRoles;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                q = q.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }

            return q.OrderByDescending(s => s.name);
        }

        public bool Create(SysRole sysRole)
        {
            try
            {
                this.context.GetTable<SysRole>().InsertOnSubmit(sysRole);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var dep = this.context.GetTable<SysRole>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<SysRole>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysRole FindById(int id)
        {
            return this.context.GetTable<SysRole>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(SysRole sysRole)
        {
            var dep = this.context.GetTable<SysRole>().FirstOrDefault(c => c.id.Equals(sysRole.id));
            if (dep != null)
            {
                dep.name = sysRole.name;
                dep.remarks = sysRole.remarks;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SysRole> GetAll()
        {
            return this.context.GetTable<SysRole>().ToList();
        }

        public List<SysRole> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<SysRole>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
