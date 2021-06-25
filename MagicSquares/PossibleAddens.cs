using Open.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MagicSquares
{
	public class PossibleAddens
	{
		public PossibleAddens()
		{
		}

		readonly ConcurrentDictionary<uint, ConcurrentDictionary<uint, IReadOnlyList<IReadOnlyList<uint>>>> Cache = new();

		public IReadOnlyList<IReadOnlyList<uint>> UniqueAddensFor(uint sum, uint count)
			=> Cache
				.GetOrAdd(count, key => new ConcurrentDictionary<uint, IReadOnlyList<IReadOnlyList<uint>>>())
				.GetOrAdd(sum, key => GetUniqueAddens(sum, count).Memoize());

		public IEnumerable<IReadOnlyList<uint>> GetUniqueAddens(uint sum, uint count)
		{
			if (count > int.MaxValue)
				throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be greater than signed 32 bit integer maximum.");
			if(count<2 || sum < 3)
				yield break;

			var capacity = (int)count;


			if (count==2)
			{
				uint i = 0;
			loop2:
				i++;
				if (i * 2 >= sum) yield break;
				yield return ImmutableArray.Create(i, sum - i);

				goto loop2;
			}

			{
				uint i = 2;
				var builder = ImmutableArray.CreateBuilder<uint>();

				while (++i < sum)
				{
					var next = sum - i;
					var addends = UniqueAddensFor(i, count - 1);
					foreach (var a in addends)
					{
						builder.Capacity = capacity;
						if (a[a.Count - 1] >= next) continue;
						builder.AddRange(a);
						builder.Add(next);
						yield return builder.MoveToImmutable();
					}
				}
			}
		}
	}
}
