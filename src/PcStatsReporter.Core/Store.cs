using System;
using System.Collections.Concurrent;

namespace PcStatsReporter.Core
{
    public class Store
    {
        private static readonly ConcurrentDictionary<Type, object> ConcurrentDictionary = new ConcurrentDictionary<Type, object>();

        public void Set<T>(T obj)
        {
            var type = typeof(T);
            ConcurrentDictionary.AddOrUpdate(type, obj, (x,y) => obj);
        }

        public T Get<T>()
        {
            var type = typeof(T);
            if (ConcurrentDictionary.TryGetValue(type, out var obj))
            {
                return (T)obj;
            }

            return default(T);
        }
    }
}
