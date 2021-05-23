using System;
using System.Linq;

namespace MDMM1
{
	/// <summary>
	/// Методы расширения для двумерной матрицы
	/// </summary>
	public static class Matrix2dExtensions
	{
		const int intMax = int.MaxValue;
		const int intMin = int.MinValue;

		/// <summary>
		/// Возвращает минимальное значение в строке матрицы
		/// </summary>
		/// <param name="matrix">Матрица</param>
		/// <param name="rowIndex">Индекс строки</param>
		public static int GetMinRowValue(this int[,] matrix, int rowIndex)
		{
			var min = intMax;
			for (int col = 0; col < matrix.GetLength(1); col++)
			{
				min = Math.Min(min, matrix[rowIndex, col]);
			}

			return min;
		}

		/// <summary>
		/// Возвращает максимальное значение в строке матрицы
		/// </summary>
		/// <param name="matrix">Матрица</param>
		/// <param name="rowIndex">Индекс строки</param>
		public static int GetMaxRowValue(this int[,] matrix, int rowIndex)
		{
			var max = intMin;
			for (int col = 0; col < matrix.GetLength(1); col++)
			{
				max = Math.Max(max, matrix[rowIndex, col]);
			}

			return max;
		}

		/// <summary>
		/// Возвращает значения строки матрицы
		/// </summary>
		/// <param name="matrix">Матрица</param>
		/// <param name="rowIndex">Индекс строки</param>
		public static T[] GetRow<T>(this T[,] matrix, int rowNum)
		{
			return Enumerable.Range(0, matrix.GetLength(1))
				.Select(x => matrix[rowNum, x])
				.ToArray();
		}
	}
}
