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

        public int CountUnRead()
        {
            return this.context.GetTable<MessageRecord>().Count(s => s.isRead == false);
        }
    }
}
