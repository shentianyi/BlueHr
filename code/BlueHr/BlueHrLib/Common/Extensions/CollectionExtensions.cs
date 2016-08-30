using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueHrLib.Common.Extensions
{
    public static class CollectionExtensions
    {

        public static bool IsEmptyOrNull(this ICollection collection)
        {
            return collection != null && collection.Count > 0;
        }
    }
}
