using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PcStatsReporter.Core.Persistence
{
    public class MemoryHold : IHold
    {
        private static readonly ConcurrentDictionary<Type, object> ConcurrentDictionary = new ConcurrentDictionary<Type, object>();

        public async Task Set<T>(T obj)
        {
            var type = typeof(T);
            ConcurrentDictionary.AddOrUpdate(type, obj, (x,y) => obj);

            await Task.CompletedTask;
        }

        public async Task<T> Get<T>()
        {
            var type = typeof(T);
            if (ConcurrentDictionary.TryGetValue(type, out var obj))
            {
                return await Task.FromResult((T)obj);
            }

            return await Task.FromResult(default(T));
        }
    }
}