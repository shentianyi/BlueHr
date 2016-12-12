using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IFullMemberRecordRepository
    {
        IQueryable<FullMemberRecord> Search(FullMemberRecordSearchModel searchModel);

        bool Create(FullMemberRecord fmr);

        FullMemberRecord FindById(int id);

        bool Update(FullMemberRecord fmr);

        bool DeleteById(int id);

        List<FullMemberRecord> GetAll();
        List<FullMemberRecord> GetAllTableName();
        List<FullMemberRecord> FindBystaffNr(string staffNr);
        FullMemberRecord FindByNr(string staffNr);
    }
}
