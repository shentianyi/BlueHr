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

            if (!string.IsNullOrEmpty(searchModel.funCode))
            {
                q = q.Where(c => c.funCode.Contains(searchModel.funCode.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                q = q.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.controlName))
            {
                q = q.Where(c => c.controlName.Contains(searchModel.controlName.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.actionName))
            {
                q = q.Where(c => c.actionName.Contains(searchModel.actionName.Trim()));
            }

            return q.OrderBy(s => s.id);
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
                //dep.authType = sysAuth.authType;
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

        //根据roleid 获取所有权限列表
        public List<SysAuthorization> GetSysAuthByRoleId(string roleId)
        {
            List<SysRoleAuthorization> allRoleAuth = this.context.GetTable<SysRoleAuthorization>().ToList();

            List<string> allAuthIds = new List<string>();
            allRoleAuth.Where(p => p.roleId == int.Parse(roleId)).ToList().ForEach(k =>
            {
                allAuthIds.Add(k.authId.ToString());
            });

            List<SysAuthorization> all = this.context.GetTable<SysAuthorization>().ToList();

            List<SysAuthorization> returns = all.Where(p => allAuthIds.Contains(p.id.ToString())).ToList();

            return returns;

        }
    }
}
