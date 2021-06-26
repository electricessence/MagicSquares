using Open.Disposable;
using System.Collections.Generic;

namespace MagicSquares
{
    internal static class ListPool<T>
    {
        public static readonly ConcurrentQueueObjectPool<List<T>> Instance = new(() => new List<T>(), h =>
        {
            h.Clear();
            if (h.Capacity > 16) h.Capacity = 16;
        }, null);
    }

    internal static class HashSetPool<T>
    {
        public static readonly ConcurrentQueueObjectPool<HashSet<T>> Instance = new(() => new HashSet<T>(), h => h.Clear(), null);
    }
}
