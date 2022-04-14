using System.Collections.Immutable;

namespace Morris.Cascades;

public class CalculatedSource<TSource, T> : ISource<T>, ISubscriber
{
	private bool IsDisposed;
	private readonly ISource<TSource> Source;
	private Func<TSource, T> Calculate = null!;
	private ImmutableHashSet<ISubscriber> Subscribers = ImmutableHashSet.Create<ISubscriber>();

	public CalculatedSource(ISource<TSource> source, Func<TSource, T> calculate)
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));

		source.Subscribe(this);
	}

	private T CachedValue = default!;
	private bool IsCached = false;

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

	void ISubscriber.Invalidate()
	{
		IsCached = false;
		foreach (ISubscriber subscriber in Subscribers)
			subscriber.Invalidate();
	}

	public void Subscribe(ISubscriber subscriber)
	{
		ArgumentNullException.ThrowIfNull(subscriber);
		Subscribers = Subscribers.Add(subscriber);
	}

	public void Unsubscribe(ISubscriber subscriber)
	{
		ArgumentNullException.ThrowIfNull(subscriber);
		Subscribers = Subscribers.Remove(subscriber);
	}

	protected virtual void Dispose(bool disposing)
	{
		IsDisposed = true;
		if (disposing)
			Source.Unsubscribe(this);
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;

		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
