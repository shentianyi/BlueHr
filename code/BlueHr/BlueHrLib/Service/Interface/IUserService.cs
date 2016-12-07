using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
   public interface IUserService
    {
        User FindById(int id);

        IQueryable<User> Search(UserSearchModel searchModel);

        bool Create(User user);

        bool Update(User user);

        bool DeleteById(int id);
          
        /// <summary>
        /// 根据Email找到用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User FindByEmail(string email);

        /// <summary>
        /// 锁定解锁用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool LockUnLock(int id);

        bool ChangePwd(int id, string pwd);

        List<User> FindByRoleId(int sysRoleId);
        List<User> GetAllTableName();
        IQueryable<User> AdvancedSearch(string allTableName, string searchConditions, string searchValueFirst);
        List<User> FindByRole(string role);
    }
}
