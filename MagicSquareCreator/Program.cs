using MagicSquares;
using System;

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

			MagicSquare.CreateFromFirst(size, first).OutputToConsole(size);
		}
	}
}
