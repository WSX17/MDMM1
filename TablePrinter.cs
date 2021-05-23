using System;
using System.Linq;
using static System.Console;

namespace MDMM1
{
	/// <summary>
	/// Принтер для вывода таблицы
	/// </summary>
	public static class TablePrinter
	{
		/// <summary>
		/// Формат отображения ячейки
		/// </summary>
		public static string CellFormat = "{0, 15}";
		/// <summary>
		/// Символ-разделитель ячеек
		/// </summary>
		public static char Separator = '|';

		/// <summary>
		/// Вывод таблицы
		/// </summary>
		/// <param name="values">Значения</param>
		/// <param name="rowHeaders">Заголовки строк</param>
		/// <param name="columnHeaders">Заголовки столбцов</param>
		/// <param name="mainCellTitle">Значение смежной ячейки заголовков</param>
		public static void PrintTable<T>(T[,] values, T[] rowHeaders, T[] columnHeaders, string mainCellTitle)
		{
			PrintCell(mainCellTitle);

			if (columnHeaders != null)
			{
				PrintRow(columnHeaders);
			}

			for (int rowIndex = 0; rowIndex < values.GetLength(0); rowIndex++)
			{
				if (rowHeaders != null)
				{
					PrintCell(rowHeaders[rowIndex]);
				}
				PrintRow(values.GetRow(rowIndex));
			}
		}

		/// <summary>
		/// Вывод строки
		/// </summary>
		public static void PrintRow<T>(T[] values)
		{
			WriteLine(String.Join(Separator, values.Select(value => String.Format(CellFormat, value))));
		}

		/// <summary>
		/// Вывод ячейки
		/// </summary>
		public static void PrintCell(object value)
		{
			Write(CellFormat, value);
			PrintSeparator();
		}

		/// <summary>
		/// Вывод символа-разделителя
		/// </summary>
		public static void PrintSeparator()
		{
			Write(Separator);
		}
	}
}
