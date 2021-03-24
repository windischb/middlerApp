using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middlerApp.API.ExtensionMethods
{
    public static class CustomStringExtensions
    {

        public static string[] Split(this string value, string split)
        {
            if (value == null)
                return new string[0];
            return value.Split(new string[1] { split }, StringSplitOptions.None);
        }

    }
}
