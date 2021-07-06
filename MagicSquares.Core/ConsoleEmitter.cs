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
		}

		readonly IDisposable _magicSquareSubscription;

		protected override void OnDispose()
		{
			_magicSquareSubscription.Dispose();
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
	}
}
