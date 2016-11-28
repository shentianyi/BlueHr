using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueHrLib.Data;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Properties;
using System.Web.Mvc;
using System.Reflection;
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Helpers.View
{
    public static class SetSearchConditionsHelper
    {
        public static void SetSearchConditions(bool? type, bool allowBlank = false)
        {
            var item = EnumHelper.GetList(typeof(SearchConditions));

            List<SelectListItem> select = new List<SelectListItem>();

            if (allowBlank)
            {
                select.Add(new SelectListItem { Text = "", Value = "" });
            }

            foreach (var it in item)
            {
                if (type.HasValue && type.ToString().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }
            ViewDataDictionary ViewData = new ViewDataDictionary();
            ViewData["searchConditionsList"] = select;
        }









        ////private static Dictionary<string, object> getobject = new Dictionary<string, object>();
        //private static BlueHrDataContext context;
        //public static List<Staff> GetAllTableName()
        //{
        //    try
        //    {
        //        return (context.GetTable<Staff>()).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public static IEnumerable<SelectListItem> SetAllTableName(bool allowBlank = false)
        //{
        //    List<SelectListItem> select = new List<SelectListItem>();

        //    IStaffService ss = new StaffService(Settings.Default.db);

        //    var Staffs = ss.GetAllTableName();

        //    if (Staffs != null)
        //    {
        //        //获取当前记录的属性
        //        foreach (var property in Staffs[0].GetType().GetProperties())
        //        {
        //            select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
        //        }
        //    }
        //    ViewDataDictionary ViewData = new ViewDataDictionary();
        //    ViewData["getAllTableNameList"] = select;

        //    return select;
        //}




        ///// <summary>
        ///// 创建对象实例
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="fullName">命名空间.类型名</param>
        ///// <param name="assemblyName">程序集</param>
        ///// <returns></returns>
        //private static T CreateInstance<T>(string fullName, string assemblyName)
        //{
        //    string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
        //    Type o = Type.GetType(path);//加载类型
        //    object obj = Activator.CreateInstance(o, true);//根据类型创建实例
        //    return (T)obj;//类型转换并返回
        //}
        ///// <summary>
        ///// 创建对象实例
        ///// </summary>
        ///// <typeparam name="T">要创建对象的类型</typeparam>
        ///// <param name="assemblyName">类型所在程序集名称</param>
        ///// <param name="nameSpace">类型所在命名空间</param>
        ///// <param name="className">类型名</param>
        ///// <returns></returns>
        //private static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        //{
        //    try
        //    {
        //        string fullName = nameSpace + "." + className;//命名空间.类型名
        //        //此为第一种写法
        //        object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
        //        return (T)ect;//类型转换并返回
        //        //下面是第二种写法
        //        //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
        //        //Type o = Type.GetType(path);//加载类型
        //        //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
        //        //return (T)obj;//类型转换并返回
        //    }
        //    catch
        //    {
        //        //发生异常，返回类型的默认值
        //        return default(T);
        //    }
        //}
        //private static BlueHrDataContext context;
        //private static Type type1 = CreateInstance<Type>("Data.Staff", "BlueHrLib");
        //private static Type obj = CreateInstance<Type>("BlueHrLib", "Data", "Staff");
    }
}