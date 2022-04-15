namespace Morris.Cascades;

public abstract class SubscriberBase : ISubscriber
{
	private readonly IChangeNotifier Source;
	protected bool IsDisposed { get; private set; }

	public SubscriberBase(IChangeNotifier source)
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
		Source.Subscribe(this);
	}

	protected abstract void SourceChanged();

	void ISubscriber.SourceChanged()
	{
		if (IsDisposed)
			return;
		SourceChanged();
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
