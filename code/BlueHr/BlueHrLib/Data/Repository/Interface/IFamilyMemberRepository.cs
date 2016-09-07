using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IFamilyMemberRepository
    {
        IQueryable<FamilyMemeber> Search(FamilyMemberSearchModel searchModel);

        bool Create(FamilyMemeber familyMember);

        FamilyMemeber FindById(int id);

        bool Update(FamilyMemeber familyMember);

        bool DeleteById(int id);

        FamilyMemeber CreateFromAjax(FamilyMemeber familyMember);
    }
}
