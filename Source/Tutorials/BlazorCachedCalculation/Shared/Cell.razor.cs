using Microsoft.AspNetCore.Components;
using BlazorCachedCalculation.Models;
using Morris.Cascade;

namespace BlazorCachedCalculation.Shared;

public partial class Cell
{
	public const string LocationPropertyName = "Location";
	[CascadingParameter(Name = LocationPropertyName)] public (int Column, int Row) Location { get; set; }
	[CascadingParameter] public SheetData Sheet { get; set; } = null!;

	private ISource<decimal> SourceCell = null!;
	protected override void OnInitialized()
	{
		base.OnInitialized();
		if (Sheet is null)
			throw new InvalidOperationException($"{nameof(Cell)} should be nested inside a component with a CascadingValue of type {nameof(SheetData)}");
		SourceCell = Sheet.Cells[Location.Column, Location.Row];
	}
}
