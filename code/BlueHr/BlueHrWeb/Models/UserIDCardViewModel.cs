using BlueHrLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueHrWeb.Models
{
    public class UserIDCardViewModel
    {
        public string nr { get; set; }
        public string id { get; set; }
        public string residenceAddress { get; set; }


        public static List<UserIDCardViewModel> Converts(List<Staff> staffs)
        {
            List<UserIDCardViewModel> l = new List<UserIDCardViewModel>();
            foreach (var i in staffs)
            {
                l.Add(UserIDCardViewModel.Convert(i));
            }
            return l;
        }

        public static UserIDCardViewModel Convert(Staff staff)
        {
            return new UserIDCardViewModel()
            {
                nr = staff.nr,
                id = staff.id,
                residenceAddress = staff.residenceAddress
            };
        }

    }
}