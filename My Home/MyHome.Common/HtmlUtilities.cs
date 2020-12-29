namespace MyHome.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;

    public static class HtmlUtilities
    {
        public static string EncodeThisPropertyForMe(string property)
        {
            return HttpUtility.HtmlEncode(property);
        }

        public static string DecodeThisPropertyForMe(string property)
        {
            return HttpUtility.HtmlDecode(property);
        }
    }
}
