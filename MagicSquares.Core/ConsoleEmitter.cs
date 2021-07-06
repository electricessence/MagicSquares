using Open.Disposable;
using System;
using System.Collections.Generic;

namespace MagicSquares.Core
{
	public class ConsoleEmitter : DisposableBase
	{
		public ConsoleEmitter(Tester tester)
		{
			_magicSquareSubscription = tester.Subscribe(OnMagicSquareFound);
			_distinctSummarySubscription = tester.DistinctSetSummary.Subscribe(OnDistinctSummary);
		}

		readonly IDisposable _magicSquareSubscription;
		readonly IDisposable _distinctSummarySubscription;

		protected override void OnDispose()
		{
			_magicSquareSubscription.Dispose();
			_distinctSummarySubscription.Dispose();
		}

		void OnMagicSquareFound((int id, SquareMatrix<int> square, bool perfect) found)
		{
			var (f, magicSquare, perfect) = found;
			var comment = perfect ? "(perfect)" : string.Empty; lock (this)
			{
				Console.WriteLine();
				Console.WriteLine("{0}: {1}", f, comment);
				magicSquare.OutputToConsole();
			}
		}

		void OnDistinctSummary((IReadOnlyList<int>, int, TimeSpan) report)
		{
			var (candidate, count, elapsed) = report;
			if (count != 0) _distinctSummarySubscription.Dispose();
			lock (this)
			{
				Console.WriteLine();
				Console.WriteLine("Candidate: [{0}]", string.Join(' ', candidate));
				Console.WriteLine("Total groupings found: {0}", count);
				Console.Write("{0} milliseconds", elapsed.TotalMilliseconds);
				Console.WriteLine();
			}
		}
	}
}
