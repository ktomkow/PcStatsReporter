using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcStatsReporter.Core.Persistence
{
    public interface IStore<T> where T : class, new()
    {
        Task<IReadOnlyList<T>> Get();
        Task<IReadOnlyList<T>> Get(Predicate<T> predicate);
        Task Add(T obj);
        Task Remove();
        Task Remove(Predicate<T> predicate);
    }
}