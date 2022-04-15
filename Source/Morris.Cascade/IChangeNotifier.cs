namespace Morris.Cascade;

public interface IChangeNotifier
{
	void Subscribe(ISubscriber subscriber);
	void Unsubscribe(ISubscriber subscriber);
}
