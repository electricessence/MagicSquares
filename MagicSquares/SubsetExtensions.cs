using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MagicSquares
{
    public static class SubsetExtensions
    {
		public static IEnumerable<T[]> GetSubsets<T>(this IReadOnlyList<T> source, int count)
		{
			if (count < 1)
				throw new System.ArgumentOutOfRangeException(nameof(count), count, "Must greater than zero.");
			if (count > source.Count)
				throw new System.ArgumentOutOfRangeException(nameof(count), count, "Must be less than or equal to the length of the source set.");

			var pool = ArrayPool<T>.Shared;
			var result = pool.Rent(count);

			try
            {

				if (count == 1)
				{
					foreach (var e in source)
					{
						result[0] = e;
						yield return result;
					}
					yield break;
				}
				var indices = new int[count];
				for (int pos = 0, index = 0; ;)
				{
					for (; pos < count; pos++, index++)
					{
						indices[pos] = index;
						result[pos] = source[index];
					}
					yield return result;
					do
					{
						if (pos == 0) yield break;
						index = indices[--pos] + 1;
					}
					while (index > source.Count - count + pos);
				}
			}
			finally
            {
				pool.Return(result);
            }
		}

		public static IEnumerable<ImmutableArray<T>> GetSubsetsImmutable<T>(this IReadOnlyList<T> source, int count)
		{
			foreach (var s in GetSubsets(source, count))
				yield return ImmutableArray.Create(s, 0, count);
		}
	}
}
