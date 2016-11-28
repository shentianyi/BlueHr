using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Implement
{

    public class SysRoleAuthViewService : ServiceBase, ISysRoleAuthViewService
    {
        private ISysRoleAuthRepository sysRoleAuthViewRep;

        public SysRoleAuthViewService(string dbString) : base(dbString)
        {
            sysRoleAuthViewRep = new SysRoleAuthViewRepository(this.Context);
        }


        public List<SysRoleAuthView> SysRoleAuthViewTree()
        {
            return sysRoleAuthViewRep.SysRoleAuthViewTree();
        }
    }
}