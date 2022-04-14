namespace Morris.Cascades;

public interface ISubscriber : IDisposable
{
	void Invalidate();
}
