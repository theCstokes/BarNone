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

        public static TAccumulate Aggregate<T, TAccumulate>(this IEnumerable<T> data, TAccumulate seed, 
            Func<TAccumulate, T, int, TAccumulate> action)
        {
            var i = 0;
            return data.Aggregate(seed, (result, item) =>
            {
                result = action(result, item, i);
                i++;
                return result;
            });
        }
    }
}
