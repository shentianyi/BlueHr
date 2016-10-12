using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    
   public class SysAuthorizationRepository : RepositoryBase<SysAuthorization>, ISysAuthorizationRepository
    {
        private BlueHrDataContext context;

        public SysAuthorizationRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<SysAuthorization> Search(SysAuthorizationSearchModel searchModel)
        {
            IQueryable<SysAuthorization> q = this.context.SysAuthorizations;

            if (!string.IsNullOrEmpty(searchModel.AuthType))
            {
                q = q.Where(c => c.authType.Contains(searchModel.AuthType.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                q = q.Where(c => c.name.Equals(searchModel.Name));
            }
            
            return q.OrderByDescending(s => s.name);
        }

        public bool Create(SysAuthorization sysAuth)
        {
            try
            {
                this.context.GetTable<SysAuthorization>().InsertOnSubmit(sysAuth);
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
            var dep = this.context.GetTable<SysAuthorization>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<SysAuthorization>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysAuthorization FindById(int id)
        {
            return this.context.GetTable<SysAuthorization>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(SysAuthorization sysAuth)
        {
            var dep = this.context.GetTable<SysAuthorization>().FirstOrDefault(c => c.id.Equals(sysAuth.id));
            if (dep != null)
            {
                dep.authType = sysAuth.authType;
                dep.name = sysAuth.name;
                dep.remarks = sysAuth.remarks; 
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //public List<SysAuthorization> FindByAbsenceType(int id)
        //{
        //    return this.context.GetTable<SysAuthorization>().Where(p => p.absenceTypeId.Equals(id)).ToList();
        //}

        public List<SysAuthorization> GetAll()
        {
            return this.context.GetTable<SysAuthorization>().ToList();
        }
    }
}
