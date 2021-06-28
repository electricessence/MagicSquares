using Open.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Open.Numeric
{
	public class Combinations
	{
		public Combinations()
		{
			Indexes = GetIndexes().Memoize();
		}

		public static IEnumerable<T> Arrange<T>(IReadOnlyList<T> source, IEnumerable<int> order)
		{
			foreach (var i in order)
				yield return source[i];
		}

		public IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> values)
		{
			var source = values is IReadOnlyList<T> v ? v : values.ToImmutableArray();
			return Indexes[source.Count].Select(c => Arrange(source, c));
		}

		public IReadOnlyList<IReadOnlyList<T>> GetMemoizedCombinations<T>(IEnumerable<T> values)
		{
			var source = values is IReadOnlyList<T> v ? v : values.ToImmutableArray();
			return Indexes[source.Count].Select(c => Arrange(source, c).Memoize()).Memoize();
		}

		IEnumerable<IReadOnlyList<ImmutableArray<int>>> GetIndexes()
		{
			yield return ImmutableArray<ImmutableArray<int>>.Empty;
			yield return ImmutableArray.Create(ImmutableArray.Create(0));
			yield return ImmutableArray.Create(ImmutableArray.Create(0, 1), ImmutableArray.Create(1, 0));

			var i = 2;
			var indexes = new List<int> { 0, 1 };

		loop:
			indexes.Add(i);
			++i;
			yield return GetIndexesCore(indexes).Memoize();
			goto loop;
		}

		IEnumerable<ImmutableArray<int>> GetIndexesCore(IReadOnlyList<int> first)
		{
			var len = first.Count;
			var combinations = Indexes[len - 1];
			for (var i = 0; i < len; i++)
			{
				var builder = ImmutableArray.CreateBuilder<int>(len);
				foreach (var comb in combinations)
				{
					builder.Capacity = len;
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

		readonly LazyList<IReadOnlyList<ImmutableArray<int>>> Indexes;

        public IReadOnlyList<ImmutableArray<int>> GetIndexes(int length) => Indexes[length];
	}
}
