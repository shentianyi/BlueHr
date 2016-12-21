using BlueHrLib.Data;
using BlueHrLib.Data.Message;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Service.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface IPartTimeJobService
    {
        IQueryable<PartTimeJob> Search(PartTimeJobSearchModel searchModel);

        PartTimeJobInfoModel GetPartTimeJobInfo(PartTimeJobSearchModel searchModel);

        PartTimeJob FindById(int id);

        bool Create(PartTimeJob PartTimeJob);

        bool Update(PartTimeJob title);

        bool DeleteById(int id);
        IQueryable<PartTimeJob> AdvancedSearch(string v1, string v2, string v3);
        List<PartTimeJob> FindBystaffNrPartTimeJob(string staffNr);
        //DateTime CalculateTime(int id);
    }
}
