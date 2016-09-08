using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    public interface ICertificateRepository
    {
        IQueryable<Certificate> Search(CertificateSearchModel searchModel);

        bool Create(Certificate certf);

        Certificate FindById(int id);

        bool Update(Certificate certf);

        bool DeleteById(int id);
    }
}
