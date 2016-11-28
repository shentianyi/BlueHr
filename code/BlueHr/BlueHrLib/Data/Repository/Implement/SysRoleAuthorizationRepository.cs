using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class SysRoleAuthorizationRepository : RepositoryBase<SysRoleAuthorization>, ISysRoleAuthorizationRepository
    {
        private BlueHrDataContext context;

        public SysRoleAuthorizationRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        //根据角色名或权限名获取权限列表
        public IQueryable<SysAuthorization> SearchByRoleAndAuth(SysRoleAuthorizationSearchModel searchModel)
        {
            IQueryable<SysAuthorization> q = this.context.SysAuthorizations;

            if (!string.IsNullOrEmpty(searchModel.AuthName))
            {
                q = q.Where(c => c.funCode.IndexOf(searchModel.AuthName.Trim()) != -1);
            }


            //if (!string.IsNullOrEmpty(searchModel.Name))
            //{
            //    q = q.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            //}

            return q.OrderByDescending(s => s.id);
        }

        public bool Create(SysRoleAuthorization roleAuth)
        {
            try
            {
                this.context.GetTable<SysRoleAuthorization>().InsertOnSubmit(roleAuth);
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
            var dep = this.context.GetTable<SysRoleAuthorization>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<SysRoleAuthorization>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public SysRoleAuthorization FindById(int id)
        {
            return this.context.GetTable<SysRoleAuthorization>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(SysRoleAuthorization absRecord)
        {
            var dep = this.context.GetTable<SysRoleAuthorization>().FirstOrDefault(c => c.id.Equals(absRecord.id));

            if (dep != null)
            {
                dep.authId = absRecord.authId;
                dep.roleId = absRecord.roleId;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //public List<AbsenceRecrod> FindByAbsenceType(int id)
        //{
        //    return this.context.GetTable<AbsenceRecrod>().Where(p => p.absenceTypeId.Equals(id)).ToList();
        //}

        public List<SysRoleAuthorization> GetAll()
        {
            return this.context.GetTable<SysRoleAuthorization>().ToList();
        }

        public List<SysRoleAuthorization> GetSysRoleAuthListByRoleName(string roleName)
        {
            return this.context.GetTable<SysRoleAuthorization>().ToList().Where(p => p.roleId.ToString() == roleName).ToList();
        }

        public List<SysRoleAuthorization> FindByRoleId(int roleId)
        {
            return this.context.GetTable<SysRoleAuthorization>().Where(p => p.roleId.Equals(roleId)).ToList();
        }

        public bool DeleteByRoleId(int roleId)
        {
            try
            {
                var roleAuths = this.context.GetTable<SysRoleAuthorization>().Where(c => c.roleId.Equals(roleId));
                if (roleAuths != null)
                {
                    foreach (var roleAuth in roleAuths)
                    {
                        this.context.GetTable<SysRoleAuthorization>().DeleteOnSubmit(roleAuth);
                        this.context.SubmitChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SysRoleAuthorization> GetAllTableName()
        {
            try
            {
                return (this.context.GetTable<SysRoleAuthorization>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
