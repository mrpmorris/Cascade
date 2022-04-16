using Microsoft.AspNetCore.Components;
using Morris.Cascade;

namespace BlazorCachedCalculation.Shared;

public partial class EditableCell : IDisposable
{
	[CascadingParameter] public ValueSource<decimal> Cell { get; set; } = null!;

	private IDisposable Subscription = null!;
	
	protected override void OnInitialized()
	{
		base.OnInitialized();
		if (Cell is null)
			throw new InvalidOperationException($"{nameof(EditableCell)} should be nested inside a component with a CascadingValue of type ISource<decimal>");
		Subscription = Cell.Subscribe<decimal>(() => InvokeAsync(StateHasChanged));
	}

	void IDisposable.Dispose()
	{
		Subscription.Dispose();
	}

	private void ValueChanged(decimal value)
	{
		Cell.Value = value;
	}
}
