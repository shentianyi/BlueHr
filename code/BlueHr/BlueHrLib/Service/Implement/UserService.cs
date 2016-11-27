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
        public UserService(string dbString) : base(dbString) { }

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
            return new UserRepository(new DataContext(this.DbString)).Create(user);
        }

        public bool DeleteById(int id)
        {
            return new UserRepository(new DataContext(this.DbString)).DeleteById(id);
        }

        public User FindByEmail(string email)
        {
            DataContext dc = new DataContext(this.DbString);
            return dc.Context.GetTable<User>().FirstOrDefault(s => s.email.Equals(email));
        }

        public User FindById(int id)
        {
            return new UserRepository(new DataContext(this.DbString)).FindById(id);
        }

        public List<User> FindByRoleId(int sysRoleId)
        {
            return new UserRepository(new DataContext(this.DbString)).FindByRoleId(sysRoleId);
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
            return new UserRepository(new DataContext(this.DbString)).Search(searchModel);

        }

        public bool Update(User user)
        {
            return new UserRepository(new DataContext(this.DbString)).Update(user);
        }
    }
}
