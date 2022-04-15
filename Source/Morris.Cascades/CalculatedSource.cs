namespace Morris.Cascades;

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

public sealed class XCalculatedSource<S1, TResult> : CalculatedSourceBase<TResult>
{
	private readonly ISource<S1> Source = null!;
	private readonly Func<S1, TResult> Calculate = null!;

	public XCalculatedSource(ISource<S1> source, Func<S1, TResult> calculate) : base(source)
	{
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
		Source = source ?? throw new ArgumentNullException(nameof(source));
	}

	protected override TResult GetCalulatedValue() => Calculate(Source.Value);
}


public sealed class XCalculatedSource<S1, S2, TResult> : CalculatedSourceBase<TResult>
{
	private readonly ISource<S1> Source1 = null!;
	private readonly ISource<S2> Source2 = null!;
	private readonly Func<S1, S2, TResult> Calculate = null!;

	public XCalculatedSource(
		ISource<S1> source1,
		ISource<S2> source2,
		Func<S1, S2, TResult> calculate)
		: base(source1, source2)
	{
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
		Source1 = source1 ?? throw new ArgumentNullException(nameof(source1));
		Source2 = source2 ?? throw new ArgumentNullException(nameof(source2));
	}

	protected override TResult GetCalulatedValue() => Calculate(Source1.Value, Source2.Value);
}



