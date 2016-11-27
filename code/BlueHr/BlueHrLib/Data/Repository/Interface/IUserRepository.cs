using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IUserRepository
    {
        IQueryable<User> Search(UserSearchModel searchModel);

        bool Create(User resignType);

        User FindById(int id);

        bool Update(User resignType);

        bool DeleteById(int id);

        List<User> FindByRoleId(int sysRoleId);
    }
}
