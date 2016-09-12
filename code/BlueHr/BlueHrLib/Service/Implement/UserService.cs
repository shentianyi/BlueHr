using System;
using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class UserService : ServiceBase, IUserService
    {



        public UserService(string dbString) : base(dbString) { }

        public bool Create(User user)
        {
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
