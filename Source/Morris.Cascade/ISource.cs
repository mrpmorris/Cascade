namespace Morris.Cascade;

public interface ISource<T> : IChangeNotifier
{
	T Value { get; }
}

