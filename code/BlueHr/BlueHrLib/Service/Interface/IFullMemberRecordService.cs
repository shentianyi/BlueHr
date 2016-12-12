using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IFullMemberRecordService
    {
        IQueryable<FullMemberRecord> Search(FullMemberRecordSearchModel searchModel);

        FullMemberRecordInfoModel GetFullMemberRecordInfo(FullMemberRecordSearchModel searchModel);

        FullMemberRecord FindById(int id);

        bool Create(FullMemberRecord title);

        bool Update(FullMemberRecord title);

        bool DeleteById(int id);

        List<FullMemberRecord> GetAll();
        List<FullMemberRecord> FindBystaffNr(string staffNr);
        FullMemberRecord FindByNr(string staffNr);
    }
}
