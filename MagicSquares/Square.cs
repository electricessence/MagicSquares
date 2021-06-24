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
		public Square(byte size, Combinations combinationSource)
		{
			;
			Size = size;
            var sizeInt = (int)size;
			Length = (ushort)(sizeInt * sizeInt);

			var values = Enumerable.Range(0, Length).ToImmutableArray();
			Combinations = (combinationSource ?? throw new ArgumentNullException(nameof(combinationSource))).GetCombinations(values).Select(GetCombination).Memoize();
			UniqueCombinations = Combinations.Where(c => c.IsPrimary).Memoize();
		}

		public byte Size { get; }

		public ushort Length { get; }

		public IReadOnlyList<Combination> Combinations { get; }

		public IReadOnlyList<Combination> UniqueCombinations { get; }


		static readonly IComparer<IReadOnlyCollection<uint>> Comparer = CollectionComparer<uint>.Ascending;
		public class Combination
		{

			internal Combination(Square square, IReadOnlyList<uint> values)
			{
				Square = square ?? throw new ArgumentNullException(nameof(square));
				if (values is null) throw new ArgumentNullException(nameof(values));
				if (values.Count != Square.Length) throw new ArgumentException($"Length of values ({values.Count}) does not match expected ({Square.Length}).", nameof(values));

				Values = values;
				Hash = GetHash(values);
				var variations = new Lazy<IReadOnlyList<uint>[]>(() =>
				{
					var variations = GetVariations().ToArray();
					Array.Sort(variations, Comparer);
					return variations;
				});

				_isPrimary = new Lazy<bool>(() => variations.Value.First().Equals(values));

				Group = new Lazy<IReadOnlyList<Lazy<Combination>>>(() =>
				{
					if (_isPrimary.Value)
					{
						return variations.Value.Select(v => new Lazy<Combination>(() => Square.GetCombination(v))).Memoize();
					}
					else
					{
						return Square.GetCombination(variations.Value.First()).Group.Value;
					}
				});
			}

			public Square Square { get; }

			public IReadOnlyList<uint> Values { get; }

			public Lazy<IReadOnlyList<Lazy<Combination>>> Group { get; }

			public string Hash { get; }

			readonly Lazy<bool> _isPrimary;
			public bool IsPrimary => _isPrimary.Value;

			public static string GetHash(IEnumerable<uint> values) => string.Join(' ', values);
			public static string GetHash(IEnumerable<int> values) => string.Join(' ', values);

			public IEnumerable<IReadOnlyList<uint>> GetVariations()
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
					yield return values;
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

		readonly ConcurrentDictionary<string, Lazy<Combination>> Registry = new();

        public Combination GetCombination(IReadOnlyList<uint> values)
			=> Registry.GetOrAdd(Combination.GetHash(values), key => new Lazy<Combination>(() => new Combination(this, values))).Value;
		public Combination GetCombination(IEnumerable<int> values)
			=> Registry.GetOrAdd(Combination.GetHash(values), key => new Lazy<Combination>(() => new Combination(this, values.Select(Convert.ToUInt32).ToImmutableArray()))).Value;
	}
}
