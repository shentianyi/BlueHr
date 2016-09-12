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

        public User FindByEmail(string email)
        {
            DataContext dc = new DataContext(this.DbString);
         return   dc.Context.GetTable<User>().FirstOrDefault(s => s.email.Equals(email));
        }
    }
}
