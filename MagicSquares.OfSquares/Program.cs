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
            var size = byte.Parse(Console.ReadLine() ?? string.Empty);
            if (size < 3)
            {
                Console.WriteLine("Invalid size.  Must be at least 3.");
                return;
            }

            var square = new Square(size);
            var emmitter = new ConsoleEmitter(square);
            var len = square.Length;
            var count = 0;

            Console.WriteLine();
            Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares of squares...", size, len);

            var sw = Stopwatch.StartNew();
            Parallel.ForEach(
                Enumerable.Range(2, 127).Shuffle().Select(v => v * v).ToImmutableArray().Subsets(len),
                new ParallelOptions { MaxDegreeOfParallelism = 2 },
                combination =>
            {
                using var subsets = combination.Subsets(size).Memoize();
                emmitter.Start(subsets, summaryHeader: $"Candidate: {string.Join(' ', combination)}");
                var c = Interlocked.Increment(ref count);
                if (c % 100 == 0)
                {
                    lock (square)
                    {
                        Console.WriteLine();
                        Console.WriteLine("{0}: {1}+ candidates tested.", sw.Elapsed, c);
                    }
                }
            });
        }
    }
}
