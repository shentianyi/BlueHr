using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Data.Model.Search
{
   public class StaffSearchModel
    {
        public string Nr { get; set; }
        public string NrAct { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int? Sex { get; set; }

        public int? JobTitleId { get; set; }
        //二级联动

        public string  companyNames { get; set; }
        public string departmentNames { get; set; }
        public string companyIds { get; set; }
        public string departmentIds { get; set; }


        public int? companyId { get; set; }
        public int? departmentId { get; set; }

        public DateTime? CompanyEmployAtFrom { get; set; }
        public DateTime? CompanyEmployAtTo { get; set; }
        public DateTime? BirthdayFrom { get; set; }
        public DateTime? BirthdayTo { get; set; }
        public bool? IsOnTrial { get; set; }

        public int? WorkStatus { get; set; }

        public List<string> StaffNrs { get; set; }

        //在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
        public User loginUser { get; set; }
    }
}
