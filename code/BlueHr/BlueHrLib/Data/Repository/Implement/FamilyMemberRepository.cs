using System;
using System.Linq;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;

namespace BlueHrLib.Data.Repository.Implement
{
    public class FamilyMemberRepository : RepositoryBase<FamilyMemeber>, IFamilyMemberRepository
    {
        private BlueHrDataContext context;

        public FamilyMemberRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public bool Create(FamilyMemeber familyMember)
        {
            try
            {
                this.context.GetTable<FamilyMemeber>().InsertOnSubmit(familyMember);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var fm = this.context.GetTable<FamilyMemeber>().FirstOrDefault(c => c.id.Equals(id));
            if (fm != null)
            {
                this.context.GetTable<FamilyMemeber>().DeleteOnSubmit(fm);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public FamilyMemeber FindById(int id)
        {
            return this.context.GetTable<FamilyMemeber>().FirstOrDefault(c => c.id.Equals(id));
        }

        public IQueryable<FamilyMemeber> Search(FamilyMemberSearchModel searchModel)
        {
            IQueryable<FamilyMemeber> familyMembers = this.context.FamilyMemeber;

            if (!string.IsNullOrEmpty(searchModel.MemberName))
            {
                familyMembers = familyMembers.Where(c => c.memberName.Contains(searchModel.MemberName.Trim()));
            }

            if (searchModel.FamilyMemberType.HasValue)
            {
                familyMembers = familyMembers.Where(c => c.familyMemberType.Equals(searchModel.FamilyMemberType));
            }

            return familyMembers;
        }

        public bool Update(FamilyMemeber familyMember)
        {
            var fm = this.context.GetTable<FamilyMemeber>().FirstOrDefault(c => c.id.Equals(familyMember.id));
            if (fm != null)
            {
                fm.memberName = familyMember.memberName;
                fm.familyMemberType = familyMember.familyMemberType;
                fm.birthday = familyMember.birthday;
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
