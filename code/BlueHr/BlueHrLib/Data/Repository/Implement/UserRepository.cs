﻿using ALinq.Dynamic;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private BlueHrDataContext context;

        public UserRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<User> Search(UserSearchModel searchModel)
        {
            IQueryable<User> users = this.context.User;

            if (!string.IsNullOrEmpty(searchModel.name))
            {
                users = users.Where(c => c.name.Contains(searchModel.name.Trim()));
            }
            if (!string.IsNullOrEmpty(searchModel.roleType))
            {
                users = users.Where(c => c.roleStr.Contains(searchModel.roleType.Trim()));
            }
            return users;
        }

        public bool Create(User user)
        {
            user.isLocked = false;
            this.context.GetTable<User>().InsertOnSubmit(user);
            this.context.SubmitChanges();
            return true;

        }

        public bool DeleteById(int id)
        {
            var user = this.context.GetTable<User>().FirstOrDefault(c => c.id.Equals(id));
            if (user != null)
            {
                this.context.GetTable<User>().DeleteOnSubmit(user);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User FindById(int id)
        {
            return this.context.GetTable<User>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(User tuser)
        {
            var user = this.context.GetTable<User>().FirstOrDefault(c => c.id.Equals(tuser.id));
            if (user != null)
            {
                user.name = tuser.name;
                user.email = tuser.email;

                //user.pwd = tuser.pwd;

                user.role = tuser.role;
                user.isLocked = tuser.isLocked;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<User> FindByRoleId(int sysRoleId)
        {
            return this.context.GetTable<User>().Where(c => c.role.Equals(sysRoleId)).ToList();
        }

        public IQueryable<User> AdvancedSearch(string AllTableName, string SearchConditions, string SearchValueFirst)
        {
            string strWhere = string.Empty;

            try
            {
                strWhere = SearchConditionsHelper.GetStrWhere("User", AllTableName, SearchConditions, SearchValueFirst);
                var q = this.context.CreateQuery<User>(strWhere);
                return q;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public List<User> FindByRole(string role)
        //{
        //    return this.context.GetTable<User>().Where(c => c.roleStr.Equals(role)).ToList();
        //}
    }
}
