using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface IPartTimeJobRepository
    {
        IQueryable<PartTimeJob> Search(PartTimeJobSearchModel searchModel);

        bool Create(PartTimeJob lr);

        bool Update(PartTimeJob lr);

        PartTimeJob FindById(int id);

        bool DeleteById(int id);
        IQueryable<PartTimeJob> AdvancedSearch(string v1, string v2, string v3);
        List<PartTimeJob> FindBystaffNrPartTimeJob(string staffNr);
    }
}
