using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class SysRoleAuthViewRepository : RepositoryBase<SysRole>, ISysRoleAuthRepository
    {
        private BlueHrDataContext context;

        public SysRoleAuthViewRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public List<SysRoleAuthView> SysRoleAuthViewTree()
        {
            return this.context.GetTable<SysRoleAuthView>().ToList();
        }
    }
}
