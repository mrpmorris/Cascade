using System.Collections.Immutable;

namespace Morris.Cascade;

public sealed class CompositeCalculatedSource<T> : CalculatedSourceBase<T>
{
	private readonly ImmutableArray<ISource<T>> Sources = ImmutableArray<ISource<T>>.Empty;
	private readonly Func<IEnumerable<T>, T> Calculate = null!;

	public CompositeCalculatedSource(IEnumerable<ISource<T>> sources, Func<IEnumerable<T>, T> calculate) : base(sources?.ToArray()!)
	{
		ArgumentNullException.ThrowIfNull(sources);
		Sources = sources.ToImmutableArray();
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
	}

	protected override T GetCalulatedValue() => Calculate(Sources.Select(x => x.Value));
}

