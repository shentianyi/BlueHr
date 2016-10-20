using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class CertificateRepository : RepositoryBase<Certificate>, ICertificateRepository
    {
        private BlueHrDataContext context;

        public CertificateRepository(IDataContextFactory dc) : base(dc)
        {
            this.context = dc.Context as BlueHrDataContext;
        }

        public bool Create(Certificate certf)
        {
            try
            {
                this.context.GetTable<Certificate>().InsertOnSubmit(certf);
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
            List<Attachment> atms = this.context.GetTable<Attachment>().Where(p => p.certificateId.Equals(id)).ToList();

            int deleteFlag = 0;
            atms.ForEach(k =>
            {
                if (k != null)
                {
                    this.context.GetTable<Attachment>().DeleteOnSubmit(k);
                    this.context.SubmitChanges();
                    deleteFlag++;
                }
            });

            Certificate cp = this.context.GetTable<Certificate>().FirstOrDefault(c => c.id.Equals(id));

            if (cp != null)
            {
                this.context.GetTable<Certificate>().DeleteOnSubmit(cp);
                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Certificate FindById(int id)
        {
            Certificate cp = this.context.GetTable<Certificate>().FirstOrDefault(c => c.id.Equals(id));
            return cp;
        }

        public IQueryable<Certificate> Search(CertificateSearchModel searchModel)
        {
            IQueryable<Certificate> certf = this.context.Certificates;
            if (!string.IsNullOrEmpty(searchModel.StaffNr))
            {
                certf = certf.Where(c => c.staffNr.Contains(searchModel.StaffNr.Trim()));
            }

            if (!string.IsNullOrEmpty(searchModel.StaffActNr))
            {
                certf = certf.Where(c => c.staffNr.Equals(searchModel.StaffActNr));
            }

            return certf;
        }

        public bool Update(Certificate certf, string delAtchIds)
        {
            Certificate cp = this.context.GetTable<Certificate>().FirstOrDefault(c => c.id.Equals(certf.id));

            if (cp != null)
            {
                cp.certificateTypeId = certf.certificateTypeId;
                cp.certiLevel = certf.certiLevel;
                cp.effectiveEnd = certf.effectiveEnd;
                cp.effectiveFrom = certf.effectiveFrom;
                cp.institution = certf.institution;
                cp.remark = certf.remark;

                //添加附件
                certf.Attachments.ToList().ForEach(m =>
                {
                    cp.Attachments.Add(new Attachment()
                    {
                        attachmentAbleId = null,
                        attachmentAbleType = "",
                        attachmentType = -1,
                        certificateId = certf.id,
                        name = m.name,
                        path = m.path
                    }); 
                }); 
               

                //删除附件
                if (!string.IsNullOrEmpty(delAtchIds))
                {
                    cp.Attachments.Where(p => delAtchIds.Contains(p.id.ToString())).ToList().ForEach(k =>
                    {
                        cp.Attachments.Remove(k);
                    });
                }

                this.context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Certificate> FindByCertificateType(int id)
        {
            return this.context.GetTable<Certificate>().Where(p => p.certificateTypeId.Equals(id)).ToList();
        }
    }
}