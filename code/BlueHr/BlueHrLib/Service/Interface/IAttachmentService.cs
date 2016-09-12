using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IAttachmentService
    {
        IQueryable<Attachment> Search(AttachmentSearchModel searchModel);

        Attachment FindById(int id);

        bool Create(Attachment title);

        bool Update(Attachment title);

        bool DeleteById(int id);

        AttachmentInfoModel GetAttachmentInfo(AttachmentSearchModel searchModel);
    }
}
