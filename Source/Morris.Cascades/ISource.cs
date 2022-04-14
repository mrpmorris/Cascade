namespace Morris.Cascades;

public interface ISource<T>
{
	T Value { get; }
	void Subscribe(ISubscriber subscriber);
	void Unsubscribe(ISubscriber subscriber);
}

