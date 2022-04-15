namespace Morris.Cascades;

public sealed class CalculatedSource<TSource, T> : ChangeNotifierBase, ISource<T>
{
	private bool IsCached = false;
	private T CachedValue = default!;
	private readonly ISource<TSource> Source = null!;
	private readonly Func<TSource, T> Calculate = null!;
	private readonly ISubscriber Subscriber = null!;

	public CalculatedSource(ISource<TSource> source, Func<TSource, T> calculate)
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
		Subscriber = new CallbackSubscriber(source, SourceChanged);
	}


	public T Value
	{
		get
		{
			if (!IsCached)
			{
				CachedValue = Calculate(Source.Value);
				IsCached = true;
			}
			return CachedValue;
		}
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (disposing)
			Subscriber.Dispose();
	}

	private void SourceChanged()
	{
		IsCached = false;
		NotifySubscribers();
	}
}
