namespace Morris.Cascades;

public static class ISourceExtensions
{
	public static IDisposable Subscribe<T>(this ISource<T> source, Action sourceChanged) =>
		new CallbackSubscriber(source, sourceChanged);
}
