using System.Collections.Immutable;

namespace Morris.Cascades;

public abstract class CalculatedSourceBase<T> : ChangeNotifierBase, ISource<T>, ISubscriber
{
	private bool IsCached = false;
	private T CachedValue = default!;
	private readonly ImmutableArray<IChangeNotifier> Sources;

	protected abstract T GetCalulatedValue();

	public CalculatedSourceBase(params IChangeNotifier[] sources)
	{
		ArgumentNullException.ThrowIfNull(sources);
		if (sources.Count() == 0)
			throw new ArgumentException("At least one source is required.");
		if (sources.Any(x => x is null))
			throw new ArgumentException("Null sources are not allowed.");

		Sources = sources.ToImmutableArray();
		SubscribeToSources();
	}

	public T Value
	{
		get
		{
			if (!IsCached)
			{
				CachedValue = GetCalulatedValue();
				IsCached = true;
			}
			return CachedValue;
		}
	}

	void ISubscriber.SourceChanged()
	{
		IsCached = false;
		NotifySubscribers();
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		if (disposing)
			UnsubscribeFromSources();
	}

	private void SubscribeToSources()
	{
		foreach (IChangeNotifier source in Sources)
			source.Subscribe(this);
	}

	private void UnsubscribeFromSources()
	{
		foreach (IChangeNotifier source in Sources)
			source.Unsubscribe(this);
	}
}
