using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{  
    public interface ICertificateTypeService
    {
        IQueryable<CertificateType> Search(CertificateTypeSearchModel searchModel);

        CertificateTypeInfoModel GetCertificateTypeInfo(CertificateTypeSearchModel searchModel);

        CertificateType FindById(int id);

        bool Create(CertificateType title);

        bool Update(CertificateType title);

        bool DeleteById(int id);

        List<CertificateType> GetAll();
        List<CertificateType> GetAllTableName();
    }
}
