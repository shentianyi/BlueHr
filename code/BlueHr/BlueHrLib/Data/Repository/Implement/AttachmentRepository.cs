using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        private BlueHrDataContext context;

        public AttachmentRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public IQueryable<Attachment> Search(AttachmentSearchModel searchModel)
        {
            //TODO
            IQueryable<Attachment> absenceRecords = this.context.Attachments;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                absenceRecords = absenceRecords.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return absenceRecords;
        }

        public bool Create(Attachment parModel)
        {
            try
            {
                this.context.GetTable<Attachment>().InsertOnSubmit(parModel);
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
            var dep = this.context.GetTable<Attachment>().FirstOrDefault(c => c.id.Equals(id));
            if (dep != null)
            {
                this.context.GetTable<Attachment>().DeleteOnSubmit(dep);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Attachment FindById(int id)
        {
            return this.context.GetTable<Attachment>().FirstOrDefault(c => c.id.Equals(id));
        }

        public bool Update(Attachment parModel)
        {
            var dep = this.context.GetTable<Attachment>().FirstOrDefault(c => c.id.Equals(parModel.id));
            if (dep != null)
            {
                dep.attachmentAbleId = parModel.attachmentAbleId;
                dep.attachmentAbleType = parModel.attachmentAbleType;
                dep.attachmentType = parModel.attachmentType;
                dep.certificateId = parModel.certificateId;
                dep.name = parModel.name;
                dep.path = parModel.path;

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
