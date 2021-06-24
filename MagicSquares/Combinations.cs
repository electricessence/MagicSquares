using Open.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MagicSquares
{

	public static class Combinations
	{
		//static IEnumerable<T> Combine<T>(T value, IEnumerable<T> rest)
		//{
		//	yield return value;
		//	foreach (var v in rest)
		//		yield return v;
		//}

		static IEnumerable<T> Arrange<T>(IReadOnlyList<T> source, IEnumerable<int> order)
		{
			foreach (var i in order)
				yield return source[i];
		}

		public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> values)
		{
			var source = values is IReadOnlyList<T> v ? v : values.ToImmutableArray();
			return Indexes[source.Count].Select(c => Arrange(source, c));
		}

		public static IReadOnlyList<IReadOnlyList<T>> GetMemoizedCombinations<T>(this IEnumerable<T> values)
		{
			var source = values is IReadOnlyList<T> v ? v : values.ToImmutableArray();
			return Indexes[source.Count].Select(c => Arrange(source, c).Memoize()).Memoize();
		}

		public static IEnumerable<T[]> GetSubsets<T>(this IReadOnlyList<T> source, int count)
		{
			if (count < 1)
				throw new System.ArgumentOutOfRangeException(nameof(count), count, "Must greater than zero.");
			if (count > source.Count)
				throw new System.ArgumentOutOfRangeException(nameof(count), count, "Must be less than or equal to the length of the source set.");
			var result = new T[count];
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

		public static IEnumerable<ImmutableArray<T>> GetSubsetsImmutable<T>(this IReadOnlyList<T> source, int count)
		{
			foreach (var s in GetSubsets(source, count))
				yield return s.ToImmutableArray();
		}

		static IEnumerable<IReadOnlyList<ImmutableArray<int>>> GetIndexes()
		{
			yield return ImmutableArray<ImmutableArray<int>>.Empty;
			yield return ImmutableArray.Create(ImmutableArray.Create(0));
			yield return ImmutableArray.Create(ImmutableArray.Create(0, 1), ImmutableArray.Create(1, 0));

			var i = 2;
			var indexes = new List<int>() { 0, 1 };

		loop:
			indexes.Add(i);
			++i;
			yield return GetIndexesCore(indexes.ToImmutableArray()).Memoize();
			goto loop;

		}

		static IEnumerable<ImmutableArray<int>> GetIndexesCore(ImmutableArray<int> first)
		{
			var len = first.Length;
			var combinations = Indexes[len - 1];
			for (var i = 0; i < len; i++)
			{
				var builder = ImmutableArray.CreateBuilder<int>(first.Length);
				foreach (var comb in combinations)
				{
					builder.Capacity = first.Length;
					builder.Add(i);
					foreach (var c in comb)
					{
						var v = first[c < i ? c : (c + 1)];
						builder.Add(v);
					}
					yield return builder.MoveToImmutable();
				}
			}
		}

		static readonly LazyList<IReadOnlyList<ImmutableArray<int>>> Indexes = GetIndexes().Memoize();

		public static IReadOnlyList<ImmutableArray<int>> GetIndexes(int length) => Indexes[length];
	}
}
