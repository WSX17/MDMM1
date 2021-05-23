using System.Linq;

namespace MDMM1
{
	/// <summary>
	/// Методы принятия решений
	/// </summary>
	public static class Methods
	{
		/// <summary>
		/// Критерий Вальда
		/// </summary>
		/// <param name="matrix">Матрица игры</param>
		/// <returns>Индекс оптимальной стратегии</returns>
		public static int WaldCriterion(int[,] matrix)
		{
			var maxRowIndex = 0;
			var maxRowValue = int.MinValue;

			for(int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var minColValue = matrix.GetMinRowValue(rowIndex);

				if(maxRowValue < minColValue)
				{
					maxRowValue = minColValue;
					maxRowIndex = rowIndex;
				}
			}

			return maxRowIndex;
		}

		/// <summary>
		/// Критерий Гурвица
		/// </summary>
		/// <param name="matrix">Матрица игры</param>
		/// <param name="k">Уровень пессимизма</param>
		/// <returns>Индекс оптимальной стратегии</returns>
		public static int HurwiczCriterion(int[,] matrix, double k)
		{
			var maxRowIndex = 0;
			var maxCriterion = double.MinValue;

			for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var maxRowValue = matrix.GetMaxRowValue(rowIndex);
				var minRowValue = matrix.GetMinRowValue(rowIndex);

				var hurwiczCriterion = k * minRowValue + (1 - k) * maxRowValue;

				if (maxCriterion < hurwiczCriterion)
				{
					maxCriterion = hurwiczCriterion;
					maxRowIndex = rowIndex;
				}
			}

			return maxRowIndex;
		}

		/// <summary>
		/// Критерий Сэвиджа
		/// </summary>
		/// <param name="matrix">Матрица игры</param>
		/// <returns>Индекс оптимальной стратегии</returns>
		public static int SavageCriterion(int[,] matrix)
		{
			var rMinRowIndex = 0;

			// Построение матрицы рисков
			int[,] rMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
			for(int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var maxRowValue = matrix.GetMaxRowValue(rowIndex);
				for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
				{
					rMatrix[rowIndex, colIndex] = maxRowValue - matrix[rowIndex, colIndex];
				}
			}

			// Поиск стратегии
			var rMinRowValue = int.MaxValue;
			for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var currRowMaxValue = rMatrix.GetMaxRowValue(rowIndex);
				if (currRowMaxValue < rMinRowValue)
				{
					rMinRowValue = currRowMaxValue;
					rMinRowIndex = rowIndex;
				}
			}

			return rMinRowIndex;
		}

		/// <summary>
		/// Критерий Лапласа
		/// </summary>
		/// <param name="matrix">Матрица игры</param>
		/// <returns>Индекс оптимальной стратегии</returns>
		public static int LaplaceCriterion(int[,] matrix)
		{
			var maxAvgRowIndex = 0;
			var maxAvgRow = double.MinValue;

			var colLength = matrix.GetLength(1);
			for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var currRowAvg = (1d / colLength) * matrix.GetRow(rowIndex).Sum();
				if(maxAvgRow < currRowAvg)
				{
					maxAvgRow = currRowAvg;
					maxAvgRowIndex = rowIndex;
				}
			}

			return maxAvgRowIndex;
		}

		/// <summary>
		/// Критерий Байеса
		/// </summary>
		/// <param name="matrix">Матрица игры</param>
		/// <param name="chances">Вероятности состояний природы</param>
		/// <returns>Индекс оптимальной стратегии</returns>
		public static int BayesCriterion(int [,] matrix, double[] chances)
		{
			var maxRowSumIndex = 0;
			var maxRowSum = double.MinValue;

			for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
			{
				var currRowSum = 0d;
				for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
				{
					currRowSum += matrix[rowIndex, colIndex] * chances[colIndex];
				}

				if(maxRowSum < currRowSum)
				{
					maxRowSum = currRowSum;
					maxRowSumIndex = rowIndex;
				}
			}

			return maxRowSumIndex;
		}
	}
}
