using BlazorCachedCalculation.Models;
using Morris.Cascade;

namespace BlazorCachedCalculation.Pages;

public partial class Spreadsheet : IDisposable
{
	private bool GenerateAutoValues = false;
	private Timer GenerateAutoValuesTimer = null;
	private SheetData Sheet = new SheetData(5, 5);

	private string ToggleAutoValuesButtonCaption => GenerateAutoValues ? "Stop generating auto values" : "Start generating auto values";

	private void ToggleAutoValues()
	{
		GenerateAutoValues = !GenerateAutoValues;
		if (GenerateAutoValues)
			GenerateAutoValuesTimer = new Timer(GenerateAutoValue, null, 0, 100);
		else
			GenerateAutoValuesTimer.Dispose();
	}

	void IDisposable.Dispose()
	{
		GenerateAutoValuesTimer.Dispose();
	}

	private void GenerateAutoValue(object? _)
	{
		var row = Random.Shared.Next(Sheet.RowCount - 1);
		var column = Random.Shared.Next(Sheet.ColumnCount - 1);
		((ValueSource<decimal>)Sheet.Cells[column, row]).Value++;
	}
}
