using BlueHrLib.Data.Model.Search;
using BlueHrLib.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Implement
{
    public class MessageRecordRepository : RepositoryBase<MessageRecord>, IMessageRecordRepository
    {
        private BlueHrDataContext context;

        public MessageRecordRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory)
        {
            this.context = dataContextFactory.Context as BlueHrDataContext;
        }

        public int CountToEmployees()
        {
            int q = this.context.MessageRecord.Where(s => s.messageType == 303).Count();
            return q;
        }

        public int CountUnRead()
        {
            return this.context.GetTable<MessageRecord>().Count(s => s.isRead == false);
        }

        public List<MessageRecord> LoginDetail()
        {
            var q = this.context.MessageRecord.Where(s => s.messageType == 401).ToList();
            return q;
        }

        public List<MessageRecord> FindBystaffNrShiftJob(string staffNr)
        {
            var q = this.context.MessageRecord.Where(s => s.messageType == 203).ToList();
            return q;
        }

        public List<MessageRecord> FindByType(int type)
        {
            var q = this.context.MessageRecord.Where(s => s.messageType == type).ToList();
            return q;
        }
    }
}
