using System.Collections.Immutable;

namespace Morris.Cascades;

public abstract class ChangeNotifierBase : IChangeNotifier, IDisposable
{
	private bool IsDisposed;
	private ImmutableHashSet<ISubscriber> Observers = ImmutableHashSet.Create<ISubscriber>();

	protected void NotifySubscribers()
	{
		var observers = Observers;
		for (int o = 0; o < observers.Count; o++)
		{
			observers.ElementAt(o).SourceChanged();
		}
	}

	public void Subscribe(ISubscriber subscriber)
	{
		Observers = Observers.Add(subscriber);
	}

	public void Unsubscribe(ISubscriber subscriber)
	{
		Observers = Observers.Remove(subscriber);
	}

	protected virtual void Dispose(bool disposing)
	{
		IsDisposed = true;
		Observers = ImmutableHashSet<ISubscriber>.Empty;
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;
		
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
