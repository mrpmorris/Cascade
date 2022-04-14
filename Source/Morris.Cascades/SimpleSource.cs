using System.Collections.Immutable;

namespace Morris.Cascades;

public class SimpleSource<T> : ISource<T>
{
	private ImmutableHashSet<ISubscriber> Subscribers = ImmutableHashSet.Create<ISubscriber>();

	private T _value = default!;
	public T Value
	{
		get => _value;
		set
		{
			_value = value;
			NotifySubscribers();
		}
	}

	private void NotifySubscribers()
	{
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
}
