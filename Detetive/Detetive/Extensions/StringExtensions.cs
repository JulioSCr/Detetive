using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Detetive.Extensions
{
    public static class StringExtensions
    {
        public static string TratarString(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return String.Empty;

            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(valor.Replace(" ", "").Replace("/", ""));
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}