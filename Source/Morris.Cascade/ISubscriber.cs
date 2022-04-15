namespace Morris.Cascade;

public interface ISubscriber : IDisposable
{
	void SourceChanged();
}
