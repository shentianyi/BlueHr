using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using BlueHrWeb.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace BlueHrWeb.Helpers.View
{
    public static class PartialViewHelper
    {
        
        /// <summary>
        /// 公司和部门
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static MvcHtmlString CompanyAndDepartmentDropDownList(this HtmlHelper htmlHelper, int? companyId, int? departmentId,string cid= "CompanyId",string did= "DepartmentId")
        {
            var list = string.Empty;

            ICompanyService cs = new CompanyService(Settings.Default.db);

            CompanySearchModel csm = new CompanySearchModel();

            List<Company> companies = cs.Search(csm).ToList();

            List<SelectListItem> companySelect = new List<SelectListItem>();

            companySelect.Add(new SelectListItem { Text = "", Value = "" });

            foreach (var company in companies)
            {
                if (companyId.HasValue && companyId.ToString().Equals(company.id))
                {
                    companySelect.Add(new SelectListItem { Text = company.name, Value = company.id.ToString(), Selected = true });
                }
                else
                {
                    companySelect.Add(new SelectListItem { Text = company.name, Value = company.id.ToString(), Selected = false });
                }
            }
            /// 生成CompanyList
            list += @"<div class='col-lg-4 col-md-4 col-sm-6 col-xs-12'>
                        <div class='marco-igroup-primary'> 
                        <span>公司</span>";
            list += htmlHelper.DropDownList(cid, companySelect).ToHtmlString();
            list += @"  </div>
                     </div>";

            IDepartmentService ds = new DepartmentService(Settings.Default.db);

            List<SelectListItem> departmentSelect = new List<SelectListItem>();

            List<Department> departments = new List<Department>();

            if (companyId.HasValue)
            {
                departments = ds.FindByCompanyId(companyId).ToList();


                departmentSelect.Add(new SelectListItem { Text = "", Value = "" });


                foreach (var department in departments)
                {
                    if (departmentId.HasValue && departmentId.ToString().Equals(department.id))
                    {
                        departmentSelect.Add(new SelectListItem { Text = department.fullName, Value = department.id.ToString(), Selected = true });
                    }
                    else
                    {
                        departmentSelect.Add(new SelectListItem { Text = department.fullName, Value = department.id.ToString(), Selected = false });
                    }
                }
            }

            /// 生成DeparmentList
            list += @"<div class='col-lg-4 col-md-4 col-sm-6 col-xs-12'>
                       <div class='marco-igroup-primary'>
                        <span>部门</span>";
            list += htmlHelper.DropDownList(did, departmentSelect).ToHtmlString();
            list += @"</div></div>";
            return new MvcHtmlString(list);
        }

        /// <summary>
        /// 是/否 下拉框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="yn"></param>
        /// <returns></returns>
        public static MvcHtmlString BoolDropDownList(this HtmlHelper htmlHelper,string id,bool? yn)
        {
            var list = string.Empty;

            List<EnumItem> item = new List<EnumItem>() { new EnumItem() { Text = "是", Value = "true" }, new EnumItem() { Text = "否", Value = "false" } };

            List<SelectListItem> select = new List<SelectListItem>();

            
                select.Add(new SelectListItem { Text = "", Value = "" });
             
            foreach (var it in item)
            {
                if (yn.HasValue && yn.ToString().ToLower().Equals(it.Value))
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = true });
                }
                else
                {
                    select.Add(new SelectListItem { Text = it.Text, Value = it.Value.ToString(), Selected = false });
                }
            }

            list += htmlHelper.DropDownList(id, select).ToHtmlString();

            return new MvcHtmlString(list);
        }
    }
}