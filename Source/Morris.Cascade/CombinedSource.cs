using System.Collections.Immutable;

namespace Morris.Cascade;

public sealed class CombinedSource<T> : CalculatedSourceBase<T>
{
	private readonly ImmutableArray<ISource<T>> Sources = ImmutableArray<ISource<T>>.Empty;
	private readonly Func<IEnumerable<T>, T> Combine = null!;

	public CombinedSource(IEnumerable<ISource<T>> sources, Func<IEnumerable<T>, T> combine) : base(sources?.ToArray()!)
	{
		ArgumentNullException.ThrowIfNull(sources);
		Sources = sources.ToImmutableArray();
		Combine = combine ?? throw new ArgumentNullException(nameof(combine));
	}

	protected override T GetCalulatedValue() => Combine(Sources.Select(x => x.Value));
}