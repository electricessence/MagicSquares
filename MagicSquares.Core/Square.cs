using Open.Collections;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MagicSquares.Core
{
	public class Square
	{
		public Square(byte size)
		{
			Size = size;
			var sizeInt = (int)size;
			Length = (ushort)(sizeInt * sizeInt);

			var values = Enumerable.Range(0, Length).ToImmutableArray();
			Permutations = values
				.PermutationsBuffered()
				.Select(p => p.Take(Length).ToImmutableArray())
				.Select(p => GetPermutation(p))
				.Memoize();
		}

		public byte Size { get; }

		public ushort Length { get; }

		public IReadOnlyList<Permutation> Permutations { get; }

		public class Permutation
		{
			internal Permutation(Square square, SquareMatrix<int> matrix)
			{
				Square = square ?? throw new ArgumentNullException(nameof(square));
				if (matrix.Length != Square.Length) throw new ArgumentException($"Length of values ({matrix.Length}) does not match expected ({Square.Length}).", nameof(matrix));

				Matrix = matrix;
				Hash = matrix.ToMatrixString();
				_msQuality = new(()=>matrix.MagicSquareQuality());

				// Implemented lazily to avoid infinite loop query.
				Orientations = new(square, matrix, GetOrientations, s => s.Orientations);
				RowPermutations = new(square, matrix, GetRowPermutations, s => s.RowPermutations);
				ColumnPermutations = new(square, matrix, GetColumnPermutations, s => s.ColumnPermutations);
				RowColumnPermutations = new(square, matrix, GetRowColumnPermutations, s => s.RowColumnPermutations);
				AllPermutations = new(square, matrix, GetAllPermutations, s => s.AllPermutations);
			}

			public class DeferredEval : IEnumerable<Permutation>
			{
				public DeferredEval(
					Square square,
					SquareMatrix<int> matrix,
					Func<IEnumerable<SquareMatrix<int>>> source,
					Func<Permutation, DeferredEval> selector)
				{
					var sorted = new Lazy<SquareMatrix<int>[]>(() =>
					{
						var o = source().Distinct().ToArray();
						Array.Sort(o);
						return o;
					});

					_isPrimary = new Lazy<bool>(() => sorted.Value[0].Equals(matrix));

					_value = new Lazy<IReadOnlyList<Lazy<Permutation>>>(() =>
					{
						var s = sorted.Value;
						if (_isPrimary.Value)
						{
							return s.Select(v => new Lazy<Permutation>(() => square.GetPermutation(v))).Memoize();
						}
						else
						{
							return selector(square.GetPermutation(s[0])).Value;
						}
					});
				}

				readonly Lazy<IReadOnlyList<Lazy<Permutation>>> _value;
				public IReadOnlyList<Lazy<Permutation>> Value => _value.Value;

				public Permutation Primary => _value.Value[0].Value;

				readonly Lazy<bool> _isPrimary;
				public bool IsPrimary => _isPrimary.Value;

				public IEnumerator<Permutation> GetEnumerator()
				{
					foreach (var p in _value.Value)
						yield return p.Value;
				}

				System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
			}


			public Square Square { get; }

			public SquareMatrix<int> Matrix { get; }

			private readonly Lazy<MagicSquareQuality> _msQuality;
			public MagicSquareQuality MagicSquareQuality => _msQuality.Value;

			public DeferredEval Orientations { get; }

			public DeferredEval RowPermutations { get; }

			public DeferredEval ColumnPermutations { get; }

			public DeferredEval RowColumnPermutations { get; }

			public DeferredEval AllPermutations { get; }

			public string Hash { get; }

			public override string ToString() => Hash;

			public IEnumerable<SquareMatrix<int>> GetOrientations()
			{
				var values = Matrix;
				yield return values;
				SquareMatrix<int> mirror = values.GetMirrorX().ToImmutableArray();
				yield return mirror;
				for (var i = 0; i < 3; i++)
				{
					values = values.GetRotatedCW().ToImmutableArray();
					yield return values;
					mirror = mirror.GetRotatedCW().ToImmutableArray();
					yield return mirror;
				}
			}

			public IEnumerable<SquareMatrix<int>> GetRowPermutations()
			{
				var size = Square.Size;
				var thisHash = Hash;
				return Matrix
					.Rows()
					.Permutations()
					.Select(p =>
					{
						var s =	SquareMatrix<int>.Create(p, size);
						return s.ToMatrixString() == thisHash ? Matrix : s;
					});
			}

			public IEnumerable<SquareMatrix<int>> GetColumnPermutations()
			{
				var size = Square.Size;
				var thisHash = Hash;
				var rows = Matrix.Rows();
				var indexes = Enumerable.Range(0, size).Permutations();
				var pool = ArrayPool<int>.Shared;
				var buffer = pool.Rent(size);
				try
				{
					foreach (var index in indexes)
					{
						var s = SquareMatrix<int>.Create(rows.Select(row =>
						{
							var i = 0;
							foreach (var c in row) buffer[index[i++]] = c;
							return buffer;
						}), size, true);

						yield return s.ToMatrixString() == thisHash ? Matrix : s;
					}
				}
				finally
				{
					pool.Return(buffer);
				}

			}

			public IEnumerable<SquareMatrix<int>> GetRowColumnPermutations()
				=> RowPermutations.Value.SelectMany(p => p.Value.ColumnPermutations.Value.Select(e => e.Value.Matrix));

			public IEnumerable<SquareMatrix<int>> GetAllPermutations()
				=> RowColumnPermutations.Value.SelectMany(p => p.Value.Orientations.Value.Select(e => e.Value.Matrix));

		}


		readonly ConcurrentDictionary<string, Lazy<Permutation>> Registry = new();

		public Permutation GetPermutation(SquareMatrix<int> values)
			=> Registry.GetOrAdd(values.ToMatrixString(), key => new Lazy<Permutation>(() => new Permutation(this, values))).Value;

		public Permutation GetPermutation(IEnumerable<IEnumerable<int>> values, bool ignoreOversize = false)
			=> GetPermutation(SquareMatrix<int>.Create(values, Size, ignoreOversize));

		public void ClearRegistry()
			=> Registry.Clear();
	}
}
