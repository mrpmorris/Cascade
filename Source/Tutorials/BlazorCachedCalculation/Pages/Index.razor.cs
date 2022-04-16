using Morris.Cascade;

namespace BlazorCachedCalculation.Pages
{
	public partial class Index
	{
		private ValueSource<int> CurrentCount = new ValueSource<int>();
		private CalculatedSource<int, int> Calculated = null!;
		private IDisposable Subscription = null!;
		private bool ShowUI;
		protected override void OnInitialized()
		{
			base.OnInitialized();
			Calculated = new(CurrentCount, x =>
			{
				Thread.Sleep(1000);
				Console.Beep();
				return x * 2;
			});
			Subscription = Calculated.Subscribe(() => InvokeAsync(StateHasChanged));
		}

		private void IncrementCalculatedValue()
		{
			CurrentCount.Value++;
		}

		void IDisposable.Dispose()
		{
			Subscription.Dispose();
		}
	}
}