using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tezo.Todo.Repositories
{
    public static class Extension
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
