namespace Morris.Cascade;

public class ValueSource<T> : ChangeNotifierBase, ISource<T>
{
	private readonly IEqualityComparer<T>? EqualityComparer;

	public ValueSource(IEqualityComparer<T>? equalityComparer = null)
	{
		EqualityComparer = equalityComparer;
	}


	private T _value = default!;
	public T Value
	{
		get => _value;
		set
		{
			if (EqualityComparer?.Equals(_value, value) == true)
				return;

			_value = value;
			NotifySubscribers();
		}
	}
}
