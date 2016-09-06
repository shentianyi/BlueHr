using BlueHrLib.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Helper
{
    public class MessageRecordTypeHelper
    {
        public static List<MessageRecordType> UniqTypes = new List<MessageRecordType>() {
            MessageRecordType.StaffToFullMemberAlert,
            MessageRecordType.StaffCertificateAlert
        };

        public static List<MessageRecordType> UrlTypes = new List<MessageRecordType>() {
            MessageRecordType.StaffAttAlert,
            MessageRecordType.StaffToFullMemberAlert
        };
    }
}
