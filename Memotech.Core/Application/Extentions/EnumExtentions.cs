using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memotech.Core.Application.Extentions
{
    public static class EnumExtentions
    {
        public static Dictionary<int, string> GetDictionary(this Type enumType)
        {
            var dict = new Dictionary<int, string>();
            foreach (var type in Enum.GetValues(enumType))
            {
                dict[(int)type] = type?.ToString() ?? "";
            }
            return dict;
        }
    }
}
