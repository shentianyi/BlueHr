using BlueHrLib.Data;
using BlueHrLib.Data.Model;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHrLib.Service.Interface
{
    public interface IFamilyMemberService
    {
        IQueryable<FamilyMemeber> Search(FamilyMemberSearchModel searchModel);

        FamilyMemeber FindById(int id);

        bool Create(FamilyMemeber fm);

        bool Update(FamilyMemeber fm);

        bool DeleteById(int id);
    }
}
