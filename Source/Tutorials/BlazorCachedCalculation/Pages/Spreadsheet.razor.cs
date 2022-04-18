using BlazorCachedCalculation.Models;
using Morris.Cascade;

namespace BlazorCachedCalculation.Pages;

public partial class Spreadsheet : IDisposable
{
	private bool IncrementRandomCells = false;
	private Timer IncrementRandomCellsTimer = null!;
	private SheetData Sheet = new SheetData(5, 8);

	private string IncrementRandomCellsButtonCaption => IncrementRandomCells ? "Stop incrementing random cells" : "Start incrementing random cells";
	private string IncrementRandomCellsButtonClass => IncrementRandomCells ? "btn-secondary" : "btn-primary";

	private void ToggleAutoValues()
	{
		IncrementRandomCells = !IncrementRandomCells;
		if (IncrementRandomCells)
			IncrementRandomCellsTimer = new Timer(GenerateAutoValue, null, 0, 250);
		else
			IncrementRandomCellsTimer.Dispose();
	}

	void IDisposable.Dispose()
	{
		IncrementRandomCellsTimer?.Dispose();
	}

	private void GenerateAutoValue(object? _)
	{
		IncrementRandomCell();
	}

	private void IncrementRandomCell()
	{
		var row = Random.Shared.Next(Sheet.RowCount - 1);
		var column = Random.Shared.Next(Sheet.ColumnCount - 1);
		((ValueSource<decimal>)Sheet.Cells[column, row]).Value++;
	}

	private string GetRowClass(int row) => row < Sheet.RowCount - 1 ? "data-row" : "data-row fixed-bottom";
	private string GetCellClass(int column) => column < Sheet.ColumnCount - 1 ? "" : "fixed-right";
}
