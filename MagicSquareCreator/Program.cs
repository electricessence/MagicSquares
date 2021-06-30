using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareGenerator

{
	class Program
	{
		static void Main()
		{
			Console.Write("Enter the size of the desired Magic Square: ");
			var size = int.Parse(Console.ReadLine());

			Console.Write("Enter the starting number: ");
			var first = int.Parse(Console.ReadLine());
			Console.WriteLine();

			var grid = MagicSquare.CreateFromFirst(size, first);
			var table = new List<string[]>();
			var colSum = new int[size];
			for (var y = size - 1; y >= 0; --y)
			{ 
				var rowSum = 0;
				var row = new string[size + 2];
				for(var x = 0; x<size; ++x)
				{
					var v = grid[x, y];
					rowSum += v;
					colSum[x] += v;
					row[x] = v.ToString();
				}
				row[^2] = "=";
				row[^1] = rowSum.ToString();
				table.Add(row);
			}
			var divider = new string[size];
			var colSumRow = colSum.Select(s => s.ToString()).ToArray();

			for (var c = 0; c < size; ++c)
			{
				var width = colSumRow[c].Length;
				divider[c] = new string('-', width);
				for (var r = 0; r<size;++r)
				{
					var value = table[r][c];
					var diff = width - value.Length;
					table[r][c] = new string(' ', diff) + value;
				}
			}

			foreach(var row in table)
			{
				Console.WriteLine(string.Join(' ', row));
			}

			Console.WriteLine(string.Join('-', divider));
			Console.WriteLine(string.Join(' ', colSumRow));
		}


	}
}
