namespace Morris.Cascade;

public sealed class CalculatedSource<T> : CalculatedSourceBase<T>
{
	private readonly ISource<T> Source = null!;
	private readonly Func<T, T> Calculate = null!;
	
	public CalculatedSource(ISource<T> source, Func<T, T> calculate) : base(source)
	{
		Source = source ?? throw new ArgumentNullException(nameof(source));
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
	}

	protected override T GetCalulatedValue() => Calculate(Source.Value);
}

