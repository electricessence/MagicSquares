using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Open.Collections;
using Open.Memory;

namespace MagicSquares
{
	public class Square
	{
		public Square(byte size)
		{
			Size = size;
            var sizeInt = (int)size;
			Length = (ushort)(sizeInt * sizeInt);

			var values = Enumerable.Range(0, Length).ToImmutableArray();
			Permutations = values.Permutations().Select(GetPermutation).Memoize();
			UniquePermutations = Permutations.Where(c => c.IsPrimary).Memoize();
		}

		public byte Size { get; }

		public ushort Length { get; }

		public IReadOnlyList<Permutation> Permutations { get; }

		public IReadOnlyList<Permutation> UniquePermutations { get; }


		static readonly IComparer<IReadOnlyCollection<int>> Comparer = CollectionComparer<int>.Ascending;
		public class Permutation
		{

			internal Permutation(Square square, IReadOnlyList<int> values)
			{
				Square = square ?? throw new ArgumentNullException(nameof(square));
				if (values is null) throw new ArgumentNullException(nameof(values));
				if (values.Count != Square.Length) throw new ArgumentException($"Length of values ({values.Count}) does not match expected ({Square.Length}).", nameof(values));

				Values = values is ImmutableArray<int> v ? v : values.ToImmutableArray();
				Hash = GetHash(values);
				var variations = new Lazy<IReadOnlyList<int>[]>(() =>
				{
					var variations = GetVariations().ToArray();
					Array.Sort(variations, Comparer);
					return variations;
				});

				_isPrimary = new Lazy<bool>(() => variations.Value[0].Equals(Values));

				Group = new Lazy<IReadOnlyList<Lazy<Permutation>>>(() =>
				{
					if (_isPrimary.Value)
					{
						return variations.Value.Select(v => new Lazy<Permutation>(() => Square.GetPermutation(v))).Memoize();
					}
					else
					{
						return Square.GetPermutation(variations.Value[0]).Group.Value;
					}
				});
			}

			public Square Square { get; }

			public IReadOnlyList<int> Values { get; }

			public Lazy<IReadOnlyList<Lazy<Permutation>>> Group { get; }

			public Permutation Primary => Group.Value[0].Value;

			public string Hash { get; }

			readonly Lazy<bool> _isPrimary;
			public bool IsPrimary => _isPrimary.Value;

			public int[,] ToXYGrid()
			{
				var size = Square.Size;
				var grid = new int[size, size];
				for(var y = 0; y<size ;++y )
				{
					for (var x = 0; x < size; ++x)
					{
						grid[x, y] = Values[x + y * size];
					}
				}
				return grid;
			}

			public static string GetHash(IEnumerable<int> values) => string.Join(' ', values);

			public IEnumerable<IReadOnlyList<int>> GetVariations()
			{
				var values = Values;
				yield return values;
				var mirror = Square.GetMirror(values).ToImmutableArray();
				yield return mirror;
				for (var i = 0; i < 3; i++)
				{
					values = Square.GetRotated(values).ToImmutableArray();
					yield return values;
					mirror = Square.GetRotated(mirror).ToImmutableArray();
					yield return mirror;
				}
			}
		}

		IEnumerable<T> GetMirror<T>(IReadOnlyList<T> items)
		{
			for (var n = 0; n < Size; ++n)
			{
				for (var i = Size - 1; i >= 0; --i)
				{
					yield return items[n * Size + i];
				}
			}
		}

		IEnumerable<T> GetRotated<T>(IReadOnlyList<T> items)
		{
			for (var i = 0; i < Size; ++i)
			{
				for (var n = Size - 1; n >= 0; --n)
				{
					yield return items[n * Size + i];
				}
			}
		}

		readonly ConcurrentDictionary<string, Lazy<Permutation>> Registry = new();

        public Permutation GetPermutation(IReadOnlyList<int> values)
			=> Registry.GetOrAdd(Permutation.GetHash(values), key => new Lazy<Permutation>(() => new Permutation(this, values))).Value;

		public Permutation GetPermutation(int[][] values, int size)
			=> GetPermutation(values.Take(size).SelectMany(e => e).ToImmutableArray());
	}
}
