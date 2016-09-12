using BlueHrLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
   public interface IUserService
    {
        /// <summary>
        /// 根据Email找到用户
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User FindByEmail(string email);
    }
}
