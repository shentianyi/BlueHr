using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AbsenceRecrodRepository : RepositoryBase<AbsenceRecrod>, IAbsenceRecrodRepository
    {
        private BlueHrDataContext context;

        public AbsenceRecrodRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<AbsenceRecrod> Search(AbsenceRecrodSearchModel searchModel)
        {
            //TODO
            IQueryable<AbsenceRecrod> absenceRecords = this.context.AbsenceRecrod;
            if (!string.IsNullOrEmpty(searchModel.absenceType))
            {
                absenceRecords = absenceRecords.Where(c => c.staffNr.Contains(searchModel.absenceType.Trim()));
            }
            return absenceRecords;
        }

        public bool Create(AbsenceRecrod absRecord)
        {
            try
            {
                this.context.GetTable<AbsenceRecrod>().InsertOnSubmit(absRecord);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            var dep = this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<AbsenceRecrod>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AbsenceRecrod FindById(int id)
        {
            return this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(AbsenceRecrod absRecord)
        {
            var dep = this.context.GetTable<AbsenceRecrod>().FirstOrDefault(c => c.id.Equals(absRecord.id));
            if (dep != null)
            {
                dep.absenceTypeId = absRecord.absenceTypeId;
                dep.duration = absRecord.duration;
                dep.durationType = absRecord.durationType;
                dep.remark = absRecord.remark;
                dep.staffNr = absRecord.staffNr;
                dep.absenceDate = absRecord.absenceDate;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<AbsenceRecrod> FindByAbsenceType(int id)
        {
            return this.context.GetTable<AbsenceRecrod>().Where(p => p.absenceTypeId.Equals(id)).ToList();
        }
    }
}
