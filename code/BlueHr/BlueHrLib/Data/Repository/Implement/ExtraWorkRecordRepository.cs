using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class ExtraWorkRecordRepository : RepositoryBase<ExtraWorkRecord>, IExtraWorkRecordRepository
    {
        private BlueHrDataContext context;

        public ExtraWorkRecordRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<ExtraWorkRecord> Search(ExtraWorkRecordSearchModel searchModel)
        {
            //TODO
            IQueryable<ExtraWorkRecord> modelList = this.context.ExtraWorkRecords;
            if (!string.IsNullOrEmpty(searchModel.staffNr))
            {
                modelList = modelList.Where(c => c.staffNr.Contains(searchModel.staffNr.Trim()));
            }
            return modelList;
        }

        public bool Create(ExtraWorkRecord parModel)
        {
            try
            {
                this.context.GetTable<ExtraWorkRecord>().InsertOnSubmit(parModel);
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
            var dep = this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<ExtraWorkRecord>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ExtraWorkRecord FindById(int id)
        {
            return this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(ExtraWorkRecord parModel)
        {
            var dep = this.context.GetTable<ExtraWorkRecord>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.extraWorkTypeId = parModel.extraWorkTypeId;
                dep.staffNr = parModel.staffNr;
                dep.duration = parModel.duration;
                dep.durationType = parModel.durationType;
                dep.otReason = parModel.otReason;

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
