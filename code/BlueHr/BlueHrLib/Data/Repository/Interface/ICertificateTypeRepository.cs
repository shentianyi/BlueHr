using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface ICertificateTypeRepository
    {
        IQueryable<CertificateType> Search(CertificateTypeSearchModel searchModel);

        bool Create(CertificateType certf);

        CertificateType FindById(int id);

        bool Update(CertificateType certf);

        bool DeleteById(int id);

        List<CertificateType> GetAll();
        List<CertificateType> GetAllTableName();
    }
}
