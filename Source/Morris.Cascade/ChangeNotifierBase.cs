using System.Collections.Immutable;

namespace Morris.Cascade;

public abstract class ChangeNotifierBase : IChangeNotifier, IDisposable
{
	private bool IsDisposed;
	private SpinLock SpinLock = new SpinLock();
	private ImmutableHashSet<ISubscriber> Subscribers = ImmutableHashSet.Create<ISubscriber>();

	protected void NotifySubscribers()
	{
		var observers = Subscribers;
		for (int o = 0; o < observers.Count; o++)
		{
			observers.ElementAt(o).SourceChanged();
		}
	}

	public void Subscribe(ISubscriber subscriber)
	{
		ModifySubscriberCollection(x => x.Add(subscriber));
	}

	public void Unsubscribe(ISubscriber subscriber)
	{
		ModifySubscriberCollection(x => x.Remove(subscriber));
	}

	public void Dispose()
	{
		if (IsDisposed)
			return;

		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		IsDisposed = true;
		ModifySubscriberCollection(_ => ImmutableHashSet<ISubscriber>.Empty);
	}

	private void ModifySubscriberCollection(Func<ImmutableHashSet<ISubscriber>, ImmutableHashSet<ISubscriber>> alterSubscribers)
	{
		bool lockTaken = false;
		while (!lockTaken)
			SpinLock.Enter(ref lockTaken);
		try
		{
			Subscribers = alterSubscribers(Subscribers);
		}
		finally
		{
			SpinLock.Exit();
		}
	}
}
