using System;
using System.Collections.Concurrent;

namespace PcStatsReporter.Core
{
    public class Store
    {
        private static ConcurrentDictionary<Type, object> concurrentDictionary = new ConcurrentDictionary<Type, object>();

        public void Set<T>(T obj)
        {
            var type = typeof(T);
            concurrentDictionary.AddOrUpdate(type, obj, (x,y) => y);
        }

        public T Get<T>()
        {
            var t = default(T);
            var type = typeof(T);
            if (concurrentDictionary.TryGetValue(type, out var obj))
            {
                return (T)obj;
            }

            return default(T);
        }
    }
}
