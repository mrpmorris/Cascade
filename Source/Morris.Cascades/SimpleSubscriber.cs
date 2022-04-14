namespace Morris.Cascades;

public class SimpleSubscriber<T> : ISubscriber
{
	private ISource<T> Source = null!;
	private Action Invalidated = null!;
	private bool IsDisposed;

	public SimpleSubscriber(ISource<T> source, Action invalidated)
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
		Invalidated = invalidated ?? throw new ArgumentNullException(nameof(invalidated));

		Source.Subscribe(this);
	}

	void ISubscriber.Invalidate()
	{
		if (IsDisposed)
			return;
		Invalidated();
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
