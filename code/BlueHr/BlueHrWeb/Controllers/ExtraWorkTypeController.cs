using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueHrWeb.CustomAttributes;
using BlueHrLib.Helper;
using BlueHrLib.Data.Enum;

namespace BlueHrWeb.Controllers
{
    public class ExtraWorkTypeController : Controller
    {
        // GET: ExtraWorkType
        [UserAuthorize]
        [RoleAndDataAuthorizationAttribute]
        public ActionResult Index()
        {
            IExtraWorkTypeService s = new ExtraWorkTypeService(Settings.Default.db);
            return View(s.All());
        }

        private void SetAllTableName(bool allowBlank = false)
        {
            List<SelectListItem> select = new List<SelectListItem>();

            IExtraWorkTypeService at = new ExtraWorkTypeService(Settings.Default.db);

            var ExtraWorkType = at.GetAllTableName();

            if (ExtraWorkType != null)
            {
                //获取当前记录的属性
                foreach (var property in ExtraWorkType[0].GetType().GetProperties())
                {
                    select.Add(new SelectListItem { Text = property.Name, Value = property.Name });
                }
            }

            ViewData["getAllTableNameList"] = select;
        }

        private void SetSearchConditions(bool? type, bool allowBlank = false)
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
            ViewData["searchConditionsList"] = select;
        }
    }
}