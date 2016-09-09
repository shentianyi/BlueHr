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
    public interface IResignRecordService
    {
        IQueryable<ResignRecord> Search(ResignRecordSearchModel searchModel);

        ResignRecordInfoModel GetResignRecordInfo(ResignRecordSearchModel searchModel);

        ResignRecord FindById(int id);

        ResultMessage Create(ResignRecord title);

        bool Update(ResignRecord title);

        bool DeleteById(int id);
    }
}
