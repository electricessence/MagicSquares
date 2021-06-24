using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Open.Collections;

namespace MagicSquares
{
	public class CombinationMap
	{
		static readonly List<CombinationMap> Combinations = new();
		private CombinationMap(int depth)
		{
			Depth = depth;
			Next = OfDepth(depth < 0 ? 0 : depth - 1);
		}
		public int Depth { get; }

		public CombinationMap Next { get; }

		public static CombinationMap OfDepth(int depth)
		{
			if (depth < Combinations.Count)
				return Combinations[depth];

			lock (Combinations)
			{
				if (depth < Combinations.Count)
					return Combinations[depth];

				var m = new CombinationMap(depth);
				Combinations.Add(m);
				return m;
			}
		}
	}



	public class IndexesX
	{
		public byte Size { get; }

		public ushort Length { get; }
		public ImmutableArray<ushort> Values { get; }

		public IndexesX(byte size)
		{
			Size = size;
			var sizeInt = (int)size;
			Length = (ushort)(sizeInt * sizeInt);

			Values = Enumerable.Range(1, Length).Cast<ushort>().ToImmutableArray();
		}

		static readonly ImmutableArray<ushort> UShort0 = ImmutableArray.Create<ushort>(0);

		public static IEnumerable<ImmutableArray<ushort>> GetCombinations(byte len)
		{
			if (len < 0) throw new ArgumentOutOfRangeException(nameof(len), len, "Must be at least zero.");
			switch (len)
			{
				case 0:
					yield return ImmutableArray<ushort>.Empty;
					break;

				case 1:
					yield return UShort0;
					break;

				default:
					for (var i = 0; i < len; i++)
					{

					}

			}
		}

		public IEnumerable<ImmutableArray<ushort>> Positions
		{
			get
			{
				yield return Values;

			}
		}

	}
}
