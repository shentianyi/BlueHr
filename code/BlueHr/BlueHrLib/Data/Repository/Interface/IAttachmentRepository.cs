using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{

    interface IAttachmentRepository
    {
        IQueryable<Attachment> Search(AttachmentSearchModel searchModel);

        bool Create(Attachment parModel);

        Attachment FindById(int id);

        bool Update(Attachment parModel);

        bool DeleteById(int id);
    }
}
