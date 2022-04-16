using Morris.Cascade;

namespace BlazorCachedCalculation.Models;

public class SheetData
{
	public int ColumnCount { get; }
	public int RowCount { get; }
	public ISource<decimal>[,] Cells { get; }

	public SheetData(int columnCount, int rowCount)
	{
		if (columnCount < 4)
			throw new ArgumentOutOfRangeException(paramName: nameof(columnCount), message: "Must be at least 4");
		if (rowCount < 4)
			throw new ArgumentOutOfRangeException(paramName: nameof(rowCount), message: "Must be at least 4");

		ColumnCount = columnCount;
		RowCount = rowCount;
		Cells = new ISource<decimal>[ColumnCount, RowCount];

		var rowCells = new List<ISource<decimal>>();
		for (int row = 0; row < RowCount - 1; row++)
		{
			rowCells.Clear();
			for (int column = 0; column < ColumnCount - 1; column++)
			{
				var cell = new ValueSource<decimal>();
				Cells[column, row] = cell;
				rowCells.Add(cell);
			}
			Cells[ColumnCount - 1, row] = new CombinedSource<decimal>(rowCells, x => x.Sum());
		}

		var columnCells = new List<ISource<decimal>>();
		for (int column = 0; column < ColumnCount; column++)
		{
			columnCells.Clear();
			for (int row = 0; row < RowCount - 1; row++)
				columnCells.Add(Cells[column, row]);
			Cells[column, RowCount - 1] = new CombinedSource<decimal>(columnCells, x => x.Sum());
		}
	}
}
