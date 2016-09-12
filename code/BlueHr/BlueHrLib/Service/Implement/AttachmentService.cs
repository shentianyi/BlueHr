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
    public class AttachmentService : ServiceBase, IAttachmentService
    {
        private IAttachmentRepository rep;

        public AttachmentService(string dbString) : base(dbString)
        {
            rep = new AttachmentRepository(this.Context);
        }

        public IQueryable<Attachment> Search(AttachmentSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(Attachment model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public Attachment FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(Attachment model)
        {
            return rep.Update(model);
        }

        public AttachmentInfoModel GetAttachmentInfo(AttachmentSearchModel searchModel)
        {
            AttachmentInfoModel info = new AttachmentInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IAttachmentRepository rep = new AttachmentRepository(dc);
            IQueryable<Attachment> results = rep.Search(searchModel);

            info.attachmentCount = dc.Context.GetTable<Attachment>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        } 
    }
}
