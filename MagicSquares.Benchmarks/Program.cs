using BenchmarkDotNet.Running;

namespace MagicSquares.Benchmarks
{
	class Program
	{
		static void Main() => BenchmarkRunner.Run<Addends>();
	}
}
