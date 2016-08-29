
using System;
using System.ComponentModel;
using System.Reflection;

namespace BlueHrLib.Helper
{

    public class EnumHelper
    {
        public static string GetDescription(Enum enumValue)
        {
            string description = string.Empty;
            FieldInfo info = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attributes = null;
            try
            {
                attributes = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            }
            catch { }
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        
    }
}
