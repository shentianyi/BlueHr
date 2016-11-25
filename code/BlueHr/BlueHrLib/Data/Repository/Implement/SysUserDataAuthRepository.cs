using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    
    public class SysUserDataAuthRepository : RepositoryBase<SysUserDataAuth>, ISysUserDataAuthRepository
    {
        private BlueHrDataContext context;

        public SysUserDataAuthRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<SysUserDataAuth> Search(SysUserDataAuthSearchModel searchModel)
        {
            IQueryable<SysUserDataAuth> q = this.context.SysUserDataAuths;
            //if (!string.IsNullOrEmpty(searchModel.Name))
            //{
            //    q = q.Where(c => c.name.Contains(searchModel.Name.Trim()));
            //}

            return q.OrderByDescending(s => s.id);
        }

        public bool Create(SysUserDataAuth sysRole)
        {
            try
            {
                this.context.GetTable<SysUserDataAuth>().InsertOnSubmit(sysRole);
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
            var dep = this.context.GetTable<SysUserDataAuth>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<SysUserDataAuth>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysUserDataAuth FindById(int id)
        {
            return this.context.GetTable<SysUserDataAuth>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(SysUserDataAuth sysRole)
        {
            var dep = this.context.GetTable<SysUserDataAuth>().FirstOrDefault(c => c.id.Equals(sysRole.id));
            if (dep != null)
            {
                dep.cmpId = sysRole.cmpId;
                dep.departId = sysRole.departId;
               
                dep.remarks = sysRole.remarks;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<SysUserDataAuth> GetAll()
        {
            return this.context.GetTable<SysUserDataAuth>().ToList();
        }
    }
}
 
