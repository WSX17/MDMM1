using System;

namespace MDMM1
{
	class Program
	{
		/// <summary>
		/// Себестоимость одного ящика
		/// </summary>
		const int _productionCost = 4500;
		/// <summary>
		/// Цена одного ящика
		/// </summary>
		const int _sellPrice = 9500;
		/// <summary>
		/// Спросы
		/// </summary>
		static readonly int[] _demands = { 60, 70, 80, 90 };
		/// <summary>
		/// Предложения
		/// </summary>
		static readonly int[] _supplies = _demands;
		/// <summary>
		/// Вероятности спросов
		/// </summary>
		static readonly double[] _demandChances = { 0.1, 0.3, 0.5, 0.1 };

		static void Main(string[] args)
		{
			var matrix = GetMatrix(_supplies.Length, _demands.Length);

			TablePrinter.PrintCell("Вер. спроса");
			TablePrinter.PrintRow(_demandChances);
			TablePrinter.PrintTable(matrix, _supplies, _demands, "Предл \\ Спрос");

			PrintTestResult("Критерий Вальда", _supplies[Methods.WaldCriterion(matrix)]);
			PrintTestResult("Критерий Гурвица", _supplies[Methods.HurwiczCriterion(matrix, 0.5)]);
			PrintTestResult("Критерий Сэвиджа", _supplies[Methods.SavageCriterion(matrix)]);
			PrintTestResult("Критерий Лапласа", _supplies[Methods.LaplaceCriterion(matrix)]);
			PrintTestResult("Критерий Байеса", _supplies[Methods.BayesCriterion(matrix, _demandChances)]);
		}

		/// <summary>
		/// Выводит результаты вычисления лучшей стратегии
		/// </summary>
		static void PrintTestResult(string methodName, int supply)
		{
			Console.WriteLine($"{methodName}: лучшая стратегия при производстве {supply} ящиков");
		}

		/// <summary>
		/// Строит платежную матрицу
		/// </summary>
		static int[,] GetMatrix(int rows, int cols)
		{
			var matrix = new int[rows, cols];

			for (int supplyIndex = 0; supplyIndex < rows; supplyIndex++)
			{
				for (int demandIndex = 0; demandIndex < cols; demandIndex++)
				{
					matrix[supplyIndex, demandIndex] = 
						CalculateProfit(_supplies[supplyIndex], _demands[demandIndex]);
				}
			}

			return matrix;
		}

		/// <summary>
		/// Вычисление прибыли
		/// </summary>
		/// <param name="supply">Размер предложения</param>
		/// <param name="demand">Размер спроса</param>
		static int CalculateProfit(int supply, int demand)
			=> Math.Min(supply, demand) * _sellPrice - supply * _productionCost;
	}
}
