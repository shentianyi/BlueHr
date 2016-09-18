using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Interface
{
    public interface ICertificateService
    {
        IQueryable<Certificate> Search(CertificateSearchModel searchModel);

        CertificateInfoModel GetCertificateInfo(CertificateSearchModel searchModel);

        Certificate FindById(int id);

        bool Create(Certificate title);

        bool Update(Certificate title, string delAtchIds);

        bool DeleteById(int id);

        List<Certificate> FindByCertificateType(int id);
    }
}
