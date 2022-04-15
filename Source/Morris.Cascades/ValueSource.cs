namespace Morris.Cascades;

public class ValueSource<T> : ChangeNotifierBase, ISource<T>
{
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
}
