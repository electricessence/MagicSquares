using Open.Collections;
using Open.Disposable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace MagicSquares.Core
{
	public class Tester : DisposableBase, IObservable<(int familyId, SquareMatrix<int> square, bool isTrue)>
	{
		public Tester(Square square)
		{
			Size = square.Size;
			Square = square;
		}

		int _plausable;
		int _families;
		int _trueMagic;

		readonly ConcurrentHashSet<string> _verification = new();

		readonly Subject<(int, SquareMatrix<int>, bool)> _subject = new();

		protected override void OnDispose()
		{
			_subject.Dispose();
			_verification.Dispose();
		}

		public int Size { get; }
		public Square Square { get; }

		public int PlausibleCount => _plausable;
		public int FamilyCount => _families;
		public int TrueCount => _trueMagic;

		public IDisposable Subscribe(IObserver<(int familyId, SquareMatrix<int> square, bool isTrue)> observer)
			=> _subject.Subscribe(observer);

		public void TestDistinctSet(IReadOnlyList<int> distinctSet)
		{
			if (distinctSet is null) throw new ArgumentNullException(nameof(distinctSet));
			if (distinctSet.Count != Square.Length) throw new ArgumentException($"Invalid set size.  Expected: {distinctSet.Count}  Actual: {Square.Length}", nameof(distinctSet));

			using var subsets = distinctSet.Subsets(Size).MemoizeUnsafe();
			var rowSets = subsets.Subsets(Size);
			Parallel.ForEach(rowSets, rowSet =>
			{
				if (!TryGetUniformRowSum(rowSet, out int sum)) return;
				if (!rowSet.SelectMany(e => e).AllDistinct()) return;
				TestPlausibleRowCombination(rowSet, sum);
			});
		}

		public void TestSetFromDistinctSubsets(IReadOnlyList<int> set)
		{
			if (set is null) throw new ArgumentNullException(nameof(set));
			Parallel.ForEach(set.Subsets(Square.Length), TestDistinctSet);
		}

		public void TestSetFromDistinctSubsets(IEnumerable<int> set)
		{
			if (set is IReadOnlyList<int> s) TestSumCombinationSubsets(s);
			else
			{
				using var s1 = set.MemoizeUnsafe();
				TestSumCombinationSubsets(s1);
			}
		}

		public void TestSumCombinationSubsets(IReadOnlyList<IReadOnlyList<int>> subsets)
		{
			if (subsets is null) throw new ArgumentNullException(nameof(subsets));

			Parallel.ForEach(subsets.GroupBy(s => s.Sum()), group =>
			{
				var sum = group.Key;
				using var rowSets = group.MemoizeUnsafe();
				if (rowSets.Take(Size).Count() < Size) return;
				TestSumCombinationSubsets(rowSets, sum);
			});
		}

		public void TestSumCombinationSubsets(IReadOnlyList<IReadOnlyList<int>> rowSets, int sum)
		{
			if (rowSets is null) throw new ArgumentNullException(nameof(rowSets));

			Parallel.ForEach(rowSets.Subsets(Size), rowSet =>
			{
				Debug.Assert(rowSet.Length == Size);
				if (!rowSet.SelectMany(e => e.Take(Size)).AllDistinct()) return;
				using var r = rowSet.Select(e => e.Take(Size).ToArray()).MemoizeUnsafe();
				TestPlausibleRowCombination(r, sum);
			});
		}

		public void TestSumCombinationSubsets(IReadOnlyList<int> set)
		{
			using var subsets = set.Subsets(Size).MemoizeUnsafe();
			TestSumCombinationSubsets(subsets);
		}

		public void TestSumCombinationSubsets(IEnumerable<int> set)
		{
			if (set is IReadOnlyList<int> s) TestSumCombinationSubsets(s);
			else
			{
				using var s1 = set.MemoizeUnsafe();
				TestSumCombinationSubsets(s1);
			}
		}

		static bool TryGetUniformRowSum(IReadOnlyList<IReadOnlyList<int>> rows, out int sum)
		{
#if DEBUG
			Debug.Assert(rows.Count == Size);
			Debug.Assert(rows.All(r => r.Count == Size));
#endif

			sum = 0;
			bool first = true;
			foreach (var row in rows)
			{
				if (first)
				{
					sum = row.Sum();
					first = false;
				}
				else if (row.Sum() != sum)
				{
					sum = 0;
					return false;
				}
			}
			return true;
		}

		public void TestPlausibleRowCombination(IReadOnlyList<IReadOnlyList<int>> rows, int sum)
		{
			if (sum == 0) throw new ArgumentException("Value of zero provided.", nameof(sum));

			Interlocked.Increment(ref _plausable);

			// Discovered a valid set!  We know the rows already add up.
			// Rearange each row (set) to see if we can get a Magic Square.

			// First provide a collection of row permutations.  Each row still adds up to the same, but just rearranged.
			using var c = rows.Select(r => r.Permutations()).MemoizeUnsafe();
			// Next, group each possible configuration of these rows and look for a winner.
			foreach (var magic in c.RowConfigurations().Where(a => a.IsSemiMagicSquare(Size, sum, true)))
			{
				// Ok! We found a semi-magic square...
				// Semi-magic squares have numerous configurations (rows and columns can rearrange) and one of them might be a true magic square. 
				// We shouldn't report semi-magic squares that are simply reconfigured. *
				var permutations = Square.GetPermutation(magic, ignoreOversize: true).AllPermutations;
				var primarySemiMagic = permutations.Primary;

				// Don't reprocess families of squares. *
				if (!_verification.Add(primarySemiMagic.Hash)) continue;

				var familyId = Interlocked.Increment(ref _families);

				// Look for any true (perfect) magic squares.
				var trueCount = 0;
				foreach (var p in permutations
					.Where(p => p.MagicSquareQuality == MagicSquareQuality.True)
					.GroupBy(p=>p.Orientations.Primary))
				{
					++trueCount;
					Interlocked.Increment(ref _trueMagic);
					_subject.OnNext((familyId, p.Key.Matrix, true));
				}

				if (trueCount == 0)
				{
					_subject.OnNext((familyId, primarySemiMagic.Matrix, false));
				}
			}

		}

	}
}
