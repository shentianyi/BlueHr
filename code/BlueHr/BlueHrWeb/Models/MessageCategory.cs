using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueHrWeb.Models
{
    public class MessageCategory
    {
        public static Dictionary<string, List<MessageRecordType>> CateListDic = new Dictionary<string, List<MessageRecordType>>() {
            {"alert", new List<MessageRecordType>() { MessageRecordType.StaffAttAlert, MessageRecordType.StaffToFullMemberAlert, MessageRecordType.StaffAttAlert } },
            {"manage", new List<MessageRecordType>() { MessageRecordType.StaffToFullMemeber , MessageRecordType.StaffResign, MessageRecordType.StaffShiftJob, MessageRecordType.StaffUpdateAttHour, MessageRecordType.StaffIdCheck} },
            {"basic", new List<MessageRecordType>() { MessageRecordType.StaffCreated,MessageRecordType.StaffDeleted, MessageRecordType.StaffBasicEdited,MessageRecordType.StaffBankEdited,MessageRecordType.StaffFamilyEdited} },
            {"all",null }
        };



    }
}