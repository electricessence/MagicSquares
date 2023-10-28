using Open.Disposable;
using System;

namespace MagicSquares.Core;

public class ConsoleEmitter : DisposableBase
{
	public ConsoleEmitter(Tester tester, Func<int, string>? transform = null)
	{
		ArgumentNullException.ThrowIfNull(tester);
		_magicSquareSubscription = tester.Subscribe(OnMagicSquareFound);
		_tester = tester;
		_transform = transform;
	}

	readonly IDisposable _magicSquareSubscription;
	private readonly Tester _tester;
	private readonly Func<int, string>? _transform;

	protected override void OnDispose()
		=> _magicSquareSubscription.Dispose();

	void OnMagicSquareFound((int id, SquareMatrix<int> square, bool perfect) found)
	{
		var (f, magicSquare, perfect) = found;
		var comment = perfect ? "(perfect)" : string.Empty; lock (_tester)
		{
			Console.WriteLine();
			Console.WriteLine("{0}: {1}", f, comment);
			if (_transform == null)
				magicSquare.OutputToConsole();
			else
				magicSquare.Transform(_transform).OutputToConsole();
		}
	}
}
