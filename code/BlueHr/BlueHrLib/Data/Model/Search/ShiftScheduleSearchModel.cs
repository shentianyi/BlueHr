using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Model.Search
{
    public class ShiftScheduleSearchModel
    {
        public string StaffNr { get; set; }

        public string StaffNrAct { get; set; }

        public DateTime? ScheduleAtFrom { get; set; }

        public DateTime? ScheduleAtEnd { get; set; }

        //* 在员工管理-员工列表、排班管理-排班管理、缺勤管理、加班管理的列表中，用户如果有权限查看列表，那么只可以查看他所管理部门中的所有员工(员工中已有部门、公司)
        public User lgUser { get; set; }
    }
}
