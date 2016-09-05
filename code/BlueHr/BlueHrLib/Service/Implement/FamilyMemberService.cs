using System.Linq;
using BlueHrLib.Data;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;

namespace BlueHrLib.Service.Implement
{
    public class FamilyMemberService : ServiceBase, IFamilyMemberService
    {
        private IFamilyMemberRepository familyMemberRep;

        public FamilyMemberService(string dbString) : base(dbString)
        {
            familyMemberRep = new FamilyMemberRepository(this.Context);
        }

        public IQueryable<FamilyMemeber> Search(FamilyMemberSearchModel searchModel)
        {
            return familyMemberRep.Search(searchModel);
        }

        public bool Create(FamilyMemeber familyMember)
        {
            return familyMemberRep.Create(familyMember);
        }

        public bool DeleteById(int id)
        {
            return familyMemberRep.DeleteById(id);
        }

        public FamilyMemeber FindById(int id)
        {
            return familyMemberRep.FindById(id);
        }

        public bool Update(FamilyMemeber familyMember)
        {
            return familyMemberRep.Update(familyMember);
        }
    }
}
