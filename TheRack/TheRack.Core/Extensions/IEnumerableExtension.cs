using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRack.Core.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> data, Action<T, int> action)
        {
            var i = 0;
            foreach (var t in data)
            {
                action(t, i);
                i++;
            }
        }
    }
}
