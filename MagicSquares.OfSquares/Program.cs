using MagicSquares.Core;
using Open.Collections;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MagicSquares.OfSquares
{
	class Program
	{
		static void Main()
		{
			Console.Write("Enter the size of the desired Magic Square: ");
			var size = byte.Parse(Console.ReadLine());
			if (size < 3)
			{
				Console.WriteLine("Invalid size.  Must be at least 3.");
				return;
			}

			var square = new Square(size);
			var len = square.Length;

			Console.Write("Enter the first number in the search range: ");
			var first = byte.Parse(Console.ReadLine());
			Console.Write("Enter the last number in the search range: ");
			var last = byte.Parse(Console.ReadLine());
			if (last < first + len - 1)
			{
				Console.WriteLine("Last must be at least the value of the tail of the possible set.");
				return;
			}

			using var tester = new Tester(square);
			using var emitter = new ConsoleEmitter(tester);

			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares of squares...", size, len);

			var squares = Enumerable.Range(1, last - first + 1).Select(v => v * v).ToImmutableArray();
			var sw = Stopwatch.StartNew();
			var plausible = tester.TestSetFromDistinctSubsets(squares);
			sw.Stop();
			Console.WriteLine();
			Console.WriteLine("Plausible tested: {0}", plausible);
			Console.WriteLine("Elasped Milliseconds: {0}", sw.Elapsed.TotalMilliseconds);
		}
	}
}
