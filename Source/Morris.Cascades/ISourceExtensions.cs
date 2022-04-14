namespace Morris.Cascades;

public static class ISourceExtensions
{
	public static IDisposable Subscribe<T>(this ISource<T> source, Action invalidated) =>
		new SimpleSubscriber<T>(source, invalidated);
}
