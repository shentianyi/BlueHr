using System;
using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using BlueHrLib.Helper;
using System.Collections.Generic;

namespace BlueHrLib.Service.Implement
{
    public class UserService : ServiceBase, IUserService
    {
        private IUserRepository userRep;

        public UserService(string dbString) : base(dbString) {
            userRep = new UserRepository(this.Context);
        }
        public IQueryable<User> AdvancedSearch(string allTableName, string searchConditions, string searchValueFirst)
        {
            return userRep.AdvancedSearch(allTableName, searchConditions, searchValueFirst);
        }

        public bool ChangePwd(int id, string pwd)
        {
            DataContext dc = new DataContext(this.DbString);
            User user = dc.Context.GetTable<User>().FirstOrDefault(s => s.id.Equals(id));
            if (user != null)
            {
                user.pwd = user.pwd = MD5Helper.Encryt(string.Format("{0}{1}", pwd, user.pwdSalt));
                dc.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(User user)
        {
            user.pwdSalt = user.GenSalt();
            user.pwd = MD5Helper.Encryt(string.Format("{0}{1}", user.pwd, user.pwdSalt));
            return userRep.Create(user);
        }

        public bool DeleteById(int id)
        {
            return userRep.DeleteById(id);
        }

        public User FindByEmail(string email)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<User>().FirstOrDefault(s => s.email.Equals(email));
        }

        public User FindById(int id)
        {
            return userRep.FindById(id);
        }

        public List<User> FindByRoleId(int sysRoleId)
        {
            return userRep.FindByRoleId(sysRoleId);
        }

        public List<User> GetAllTableName()
        {
            try
            {
                return (this.Context.Context.GetTable<User>()).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool LockUnLock(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            User user= dc.Context.GetTable<User>().FirstOrDefault(s => s.id.Equals(id));
            if (user != null)
            {
                user.isLocked = !user.isLocked;
                dc.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<User> Search(UserSearchModel searchModel)
        {
            return userRep.Search(searchModel);

        }

        public bool Update(User user)
        {
            return userRep.Update(user);
        }

        //public List<User> FindByRole(string role)
        //{
        //    return userRep.FindByRole(role);
        //}
    }
}
