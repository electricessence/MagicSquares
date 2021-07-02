using MagicSquares.Core;
using Open.Collections;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace MagicSquares.OfSquares
{
	class Program
	{
		static void Main()
		{
			Console.Write("Enter the size of the desired Magic Square: ");
			var size = byte.Parse(Console.ReadLine() ?? string.Empty);
			if (size < 3)
			{
				Console.WriteLine("Invalid size.  Must be at least 3.");
				return;
			}

			var square = new Square(size);
			var emmitter = new ConsoleEmitter(square);
			var len = square.Length;

			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares of squares...", size, len);

			var allSquares = Enumerable.Range(0, 256).Select(v => v * v).ToImmutableArray();
			Parallel.ForEach(allSquares.Subsets(len), combination =>
			{
				emmitter.Start(combination.Subsets(size).Memoize(), summaryHeader: $"Candidate: {string.Join(' ', combination)}");
				square.ClearRegistry();
			});
		}
	}
}
