namespace Morris.Cascade;

public class ValueSource<T> : ChangeNotifierBase, ISource<T>
{
	private readonly Func<T?, T?, bool>? AreEqual;

	public ValueSource(Func<T?, T?, bool>? areEqual = null)
	{
		AreEqual = areEqual;
	}

	public static ValueSource<T> Create(IEqualityComparer<T> equalityComparer)
	{
		ArgumentNullException.ThrowIfNull(equalityComparer);
		return new ValueSource<T>(equalityComparer.Equals);
	}


	private T _value = default!;
	public T Value
	{
		get => _value;
		set
		{
			if (AreEqual?.Invoke(_value, value) == true)
				return;

			_value = value;
			NotifySubscribers();
		}
	}
}
