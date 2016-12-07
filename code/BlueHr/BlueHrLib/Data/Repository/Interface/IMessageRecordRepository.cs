using BlueHrLib.Data.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Data.Repository.Interface
{
    interface IMessageRecordRepository
    {
        int CountUnRead();
        int CountToEmployees();
        List<MessageRecord> LoginDetail(); 
        List<MessageRecord> FindBystaffNrShiftJob(string staffNr);
        List<MessageRecord> FindByType(int type);
    }
}