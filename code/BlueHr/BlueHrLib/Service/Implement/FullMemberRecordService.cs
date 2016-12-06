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
    public class FullMemberRecordService : ServiceBase, IFullMemberRecordService
    {
        public FullMemberRecordService(string dbString) : base(dbString) { }

        public bool Create(FullMemberRecord fmr)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.Create(fmr);
        }

        public bool DeleteById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.DeleteById(id);
        }

        public FullMemberRecord FindById(int id)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.FindById(id);
        }

        public FullMemberRecordInfoModel GetFullMemberRecordInfo(FullMemberRecordSearchModel searchModel)
        {
            FullMemberRecordInfoModel info = new FullMemberRecordInfoModel();
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            IQueryable<FullMemberRecord> fmrs = fmrRep.Search(searchModel);

            info.FullMemberRecordCount = dc.Context.GetTable<FullMemberRecord>().Where(c => c.id.Equals(fmrs.Count() > 0 ? fmrs.First().id : -1)).Count();

            return info;
        }

        public IQueryable<FullMemberRecord> Search(FullMemberRecordSearchModel searchModel)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.Search(searchModel);
        }

        public bool Update(FullMemberRecord fmr)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.Update(fmr);
        }

        public List<FullMemberRecord> GetAll()
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.GetAll();
        }

        public List<FullMemberRecord> FindBystaffNr(string staffNr)
        {
            DataContext dc = new DataContext(this.DbString);
            IFullMemberRecordRepository fmrRep = new FullMemberRecordRepository(dc);
            return fmrRep.FindBystaffNr(staffNr);
        }
    }
}
