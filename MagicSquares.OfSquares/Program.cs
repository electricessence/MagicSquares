using Open.Collections;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
			}

			var square = new Square(size);
			var len = square.Length;
			var values = Enumerable.Range(1, len).Select(e => e * e).ToArray();
			Console.WriteLine();
			Console.WriteLine("Searching for other {0} x {0} ({1} unique) Magic Squares of Squares...", size, len);

			var verification = new HashSet<string>();
			foreach (var e in values.Permutations().MagicSquares(size))
			{
				var p = square.GetPermutation(e);
				if (p.IsPrimary)
				{
					var isNew = verification.Add(p.Hash);
					Debug.Assert(isNew);
					if(!isNew) continue;
				}
				Console.WriteLine();
				Console.WriteLine("{0}:", verification.Count);
				p.ToXYGrid().OutputToConsole(size);
			}

			Console.WriteLine();
			Console.WriteLine("Total found: {0}", verification.Count);
		}

	}
}
