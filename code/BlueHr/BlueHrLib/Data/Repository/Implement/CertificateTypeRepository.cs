using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class CertificateTypeRepository : RepositoryBase<CertificateType>, ICertificateTypeRepository
    {
        private BlueHrDataContext context;

        public CertificateTypeRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(CertificateType certf)
        {
            try
            {
                this.context.GetTable<CertificateType>().InsertOnSubmit(certf);
                this.context.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            CertificateType cp = this.context.GetTable<CertificateType>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<CertificateType>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public CertificateType FindById(int id)
        {
            CertificateType cp = this.context.GetTable<CertificateType>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<CertificateType> Search(CertificateTypeSearchModel searchModel)
        {
            IQueryable<CertificateType> certf = this.context.CertificateType;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                certf = certf.Where(c => c.name.Contains(searchModel.Name.Trim()));
            }
            return certf;
        }

        public bool Update(CertificateType certf)
        {
            CertificateType cp = this.context.GetTable<CertificateType>().FirstOrDefault(c => c.id.Equals(certf.id));

            if (cp != null)
            {
                cp.name = certf.name;
                cp.remark = certf.remark;
                cp.isNecessary = certf.isNecessary;
                cp.isSystem = certf.isSystem;
                cp.systemCode = certf.systemCode;

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<CertificateType> GetAll()
        {
            return this.context.GetTable<CertificateType>().ToList();
        }
    }
}