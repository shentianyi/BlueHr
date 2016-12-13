using BlueHrLib.Data;
using BlueHrLib.Data.Model.PageViewModel;
using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Implement;
using BlueHrLib.Data.Repository.Interface;
using BlueHrLib.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Service.Implement
{
    public class CertificateService : ServiceBase, ICertificateService
    {
        private ICertificateRepository rep;

        public CertificateService(string dbString) : base(dbString)
        {
            rep = new CertificateRepository(this.Context);
        }

        public IQueryable<Certificate> Search(CertificateSearchModel searchModel)
        {
            return rep.Search(searchModel);
        }

        public bool Create(Certificate model)
        {
            return rep.Create(model);
        }

        public bool DeleteById(int id)
        {
            return rep.DeleteById(id);
        }

        public Certificate FindById(int id)
        {
            return rep.FindById(id);
        }

        public bool Update(Certificate model, string delAtchIds)
        {
            return rep.Update(model, delAtchIds);
        }

        public CertificateInfoModel GetCertificateInfo(CertificateSearchModel searchModel)
        {
            CertificateInfoModel info = new CertificateInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ICertificateRepository rep = new CertificateRepository(dc);
            IQueryable<Certificate> results = rep.Search(searchModel);

            info.certificateCount = dc.Context.GetTable<Certificate>().Where(c => c.id.Equals(results.Count() > 0 ? results.First().id : -1)).Count();

            return info;
        }

        public List<Certificate> FindByCertificateType(int id)
        {
            return rep.FindByCertificateType(id);
        }

        public List<Certificate> GetAllTableName()
        {
            return rep.GetAllTableName();
        }

        public List<Certificate> FindByStaffNr(string staffNr)
        {
            return rep.FindByStaffNr(staffNr);
        }
    }
}