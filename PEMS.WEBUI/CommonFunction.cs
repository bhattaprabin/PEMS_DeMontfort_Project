using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEMS.WEBUI
{
    public static class CommonFunction
    {
        public static string ConvertToString(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";
            return obj.ToString().Trim();
        }
        public static string ConvertToString(this object obj, string format)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;

            return string.Format("{0:" + format + "}", obj);
        }
    }
}