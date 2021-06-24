using Open.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace MagicSquares
{
	class Program
	{
		static void Main()
		{

			byte size = 4;
			var square = new Square(size);
			var sums = new SumCombinations();
			var len = square.Length;


			Parallel.For(6, 500, u =>
			{
				var n = (uint)u;
				var s = sums.UniqueAddensFor(n, size);
				if (s.Count < square.Length) return;

				// Get N rows and test them.
				var subsets = s.GetSubsetsImmutable(size).Memoize();
				foreach (var rows in subsets)
				{
					if (AreEntriesUnique(rows))
					{
						var p = new HashSet<Square.Combination>();
						var c = rows.Select(r => r.GetMemoizedCombinations()).Memoize();
						foreach (var combo in Arrangments(c).Skip(1).Where(a => IsMagicColumns(a, n)))
						{
							var ms = square.GetCombination(combo.SelectMany(e => e).ToImmutableArray());
							var pCombo = ms.Group.Value.First().Value;
							if (!p.Add(pCombo)) continue;
							lock (square)
							{
								Console.Write("Magic No: {0}", n);
								for (var i = 0; i < len; i++)
								{
									if (i % size == 0) Console.WriteLine();
									else Console.Write(' ');
									Console.Write(pCombo.Values[i]);
								}

								Console.WriteLine();
								Console.ReadLine();
							}

						}
						p.Clear();
					}
				}
			});

			Console.WriteLine("Done.");

			bool AreEntriesUnique(ImmutableArray<IReadOnlyList<uint>> rows)
			{
				var d = new HashSet<uint>();

				foreach (var row in rows)
				{
					foreach (var e in row)
					{
						if (!d.Add(e)) return false;
					}
				}

				return true;
			}

			bool IsMagicColumns(IReadOnlyList<IReadOnlyList<uint>> sq, uint sum)
			{
				for (var i = 0; i < size; i++)
					if (SumColumn(sq, i) != sum) return false;
				return true;
			}

			//foreach (var c in square.Combinations)
			//{
			//	if (c.IsPrimary)
			//		Console.WriteLine(c.Hash);
			//	else
			//		Console.WriteLine("{0} => {1}", c.Hash, c.Group.Value.First().Value.Hash);
			//}


			//Console.WriteLine("Total Unique Combinations: {0} of {1}", square.UniqueCombinations.Count, square.Combinations.Count);


		}

		static uint SumColumn(IEnumerable<IReadOnlyList<uint>> square, int c)
		{
			uint sum = 0;
			foreach (var row in square)
				sum += row[c];
			return sum;
		}

		static IEnumerable<T[]> Arrangments<T>(IReadOnlyList<IEnumerable<T>> source)
		{
			List<IEnumerator<T>> enumerators = new();
			foreach (var e in source.Select(e => e.GetEnumerator()))
			{
				if (!e.MoveNext())
				{
					yield break;
				}
				enumerators.Add(e);
			}

			var count = enumerators.Count;
			var buffer = new T[count];

			bool GetNext()
			{
				var reset = new List<int>();
				for (var i = 0; i < count; i++)
				{
					var e = enumerators[i];
					if (e.MoveNext())
					{
						foreach (var r in reset)
						{
							enumerators[r] = e = source[r].GetEnumerator();
							e.MoveNext();
						}
						return true;
					}
					e.Dispose();
					if (i == count - 1) break;
					reset.Add(i);
				}
				reset.Clear();
				return false;
			}

			do
			{
				for (var i = 0; i < count; i++)
				{
					buffer[i] = enumerators[i].Current ?? throw new NullReferenceException();
				}
				yield return buffer;
			}
			while (GetNext());


		}
	}
}
