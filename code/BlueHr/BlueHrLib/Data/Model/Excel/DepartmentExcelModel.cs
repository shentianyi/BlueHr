using BlueHrLib.Data.Message;
using BlueHrLib.Service.Implement;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Excel
{
    public class DepartmentExcelModel : BaseExcelModel
    {
        public static List<string> Headers = new List<string>() { "名称", "备注", "上级部门名称", "公司名称"};
        

        public string Name { get; set; }
        public string Remark { get; set; }
        public string ParentDepartmentName { get; set; }
        public string CompanyName { get; set; }

        public BlueHrLib.Data.Department ParentDepartment { get; set; }
        public Company Company { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dbString"></param>
        public void Validate(string dbString)
        {
            ValidateMessage msg = new ValidateMessage();

            if (string.IsNullOrEmpty(this.Name))
            {
                msg.Contents.Add("名称不可空");
            }

            if (string.IsNullOrEmpty(this.CompanyName))
            {
                msg.Contents.Add("公司名称不可空");
            }
            else
            {
                ICompanyService cs = new CompanyService(dbString);
                Company c = cs.FindByName(this.CompanyName);
                if (c == null)
                {
                    msg.Contents.Add("公司不存在");
                }
                else
                {
                     this.Company = c;
                    //IDepartmentService ds = new DepartmentService(dbString);
                    //BlueHrLib.Data.Department d = ds.FindByIdWithCompanyId(this.Company.id, this.ParentDepartmentName);
                    //if (d != null)
                    //{
                    //    msg.Contents.Add("部门已存在，不可重复");
                    //}
                }
            }




            //if (msg.Contents.Count == 0)
            //{
            //    if (!string.IsNullOrEmpty(this.ParentDepartmentName))
            //    {
            //        IDepartmentService ds = new DepartmentService(dbString);
            //        BlueHrLib.Data.Department d = ds.FindByIdWithCompanyId(this.Company.id, this.ParentDepartmentName);
            //        if (d == null)
            //        {
            //            msg.Contents.Add("上级部门不存在");
            //        }
            //        else
            //        {
            //            this.ParentDepartment = d;
            //        }
            //    }
            //}

            msg.Success = msg.Contents.Count == 0;

            this.ValidateMessage = msg;
        }


        public static List<BlueHrLib.Data.Department> Convert(List<DepartmentExcelModel> models)
        {
            List<BlueHrLib.Data.Department> records = new List<BlueHrLib.Data.Department>();

            models.ForEach(p =>
            {
                bool hasValid = p.ValidateMessage != null && p.ValidateMessage.Success;

                if (hasValid)
                {
                    BlueHrLib.Data.Department d = new BlueHrLib.Data.Department();
                    d.name = p.Name;
                    d.remark = p.Remark;
                    d.companyId = p.Company.id;
                    d.parentId = p.ParentDepartment.id;
                    records.Add(d);
                }  
            });

            return records;
        }
    }
}
