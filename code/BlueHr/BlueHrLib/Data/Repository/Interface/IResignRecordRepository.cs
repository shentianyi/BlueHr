using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IResignRecordRepository
    {
        IQueryable<ResignRecord> Search(ResignRecordSearchModel searchModel);

        bool Create(ResignRecord rsr);

        bool Update(ResignRecord rsr);

        ResignRecord FindById(int id);

        bool DeleteById(int id);

        List<ResignRecord> FindByResignType(int id);
    }
}
