using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AbsenceTypeRepository : RepositoryBase<AbsenceType>, IAbsenceTypeRepository
    {
        private BlueHrDataContext context;

        public AbsenceTypeRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<AbsenceType> Search(AbsenceTypeSearchModel searchModel)
        {
            IQueryable<AbsenceType> absenceTypes = this.context.AbsenceType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                absenceTypes = absenceTypes.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return absenceTypes;
        }

        public bool Create(AbsenceType absenceType)
        {
            try
            {
                this.context.GetTable<AbsenceType>().InsertOnSubmit(absenceType);
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
            var dep = this.context.GetTable<AbsenceType>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<AbsenceType>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AbsenceType FindById(int id)
        {
            return this.context.GetTable<AbsenceType>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(AbsenceType absenceType)
        {
            var dep = this.context.GetTable<AbsenceType>().FirstOrDefault(c => c.id.Equals(absenceType.id));
            if (dep != null)
            {
                dep.name = absenceType.name;
                dep.remark = absenceType.remark;
                dep.code = absenceType.code;
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
