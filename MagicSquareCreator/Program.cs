using System;

namespace MagicSquareCreator
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

			var colSum = new int[size];
			for (var y = size - 1; y >= 0; --y)
			{ 
				var rowSum = 0;
				for(var x = 0; x<size; ++x)
				{
					var v = grid[x, y];
					rowSum += v;
					colSum[x] += v;
					Console.Write(v);
					Console.Write(' ');
				}
				Console.Write(" = ");
				Console.WriteLine(rowSum);
			}
			Console.WriteLine("_____________");
			Console.WriteLine(string.Join(' ', colSum));
		}


	}
}
