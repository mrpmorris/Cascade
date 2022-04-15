namespace Morris.Cascades;

public interface ISource<T> : IChangeNotifier
{
	T Value { get; }
}

