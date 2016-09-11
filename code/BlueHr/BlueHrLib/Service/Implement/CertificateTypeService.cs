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
    public class CertificateTypeService : ServiceBase, ICertificateTypeService
    {
        public CertificateTypeService(string dbString) : base(dbString) { }

        public bool Create(CertificateType certf)
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.Create(certf);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.DeleteById(id);
        }

        public CertificateType FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.FindById(id);
        }

        public CertificateTypeInfoModel GetCertificateTypeInfo(CertificateTypeSearchModel searchModel)
        {
            CertificateTypeInfoModel info = new CertificateTypeInfoModel();
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            IQueryable<CertificateType> certfs = certfRep.Search(searchModel);

            info.certfCount = dc.Context.GetTable<CertificateType>().Where(c => c.id.Equals(certfs.Count() > 0 ? certfs.First().id : -1)).Count();

            return info;
        }

        public IQueryable<CertificateType> Search(CertificateTypeSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.Search(searchModel);
        }

        public bool Update(CertificateType certf)
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.Update(certf);
        }

        public List<CertificateType> GetAll()
        {
            DataContext dc = new DataContext(this.DbString);
            ICertificateTypeRepository certfRep = new CertificateTypeRepository(dc);
            return certfRep.GetAll();
        }

    }
}
