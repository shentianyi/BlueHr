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
        /// <param name="htmlHeper"></param>
        /// <param name="companyId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static MvcHtmlString CompanyAndDepartmentDropDownList(this HtmlHelper htmlHeper, int? companyId, int? departmentId)
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
                        <div class='marco-igroup-danger'> 
                        <span>公司*</span>";
            list += htmlHeper.DropDownList("CompanyId", companySelect).ToHtmlString();
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
                        departmentSelect.Add(new SelectListItem { Text = department.name, Value = department.id.ToString(), Selected = true });
                    }
                    else
                    {
                        departmentSelect.Add(new SelectListItem { Text = department.name, Value = department.id.ToString(), Selected = false });
                    }
                }
            }

            /// 生成DeparmentList
            list += @"<div class='col-lg-4 col-md-4 col-sm-6 col-xs-12'>
                       <div class='marco-igroup-danger'>
                        <span>部门*</span>";
            list += htmlHeper.DropDownList("DepartmentId", departmentSelect).ToHtmlString();
            list += @"</div></div>";
            return new MvcHtmlString(list);
        }
    }
}