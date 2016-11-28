using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IWorkAndRestRepository
    {
        IQueryable<WorkAndRest> Search(WorkAndRestSearchModel searchModel);

        bool Create(WorkAndRest ws);

        bool Update(WorkAndRest ws);

        WorkAndRest FindById(int id);

        bool DeleteById(int id);

        bool HasDateAtExist(DateTime? datetime);
        List<WorkAndRest> GetAllTableName();
    }
}
