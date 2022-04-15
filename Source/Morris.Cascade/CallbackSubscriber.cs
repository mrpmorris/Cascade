namespace Morris.Cascade;

public class CallbackSubscriber : SubscriberBase
{
	private readonly Action Callback = null!;

	public CallbackSubscriber(IChangeNotifier source, Action callback) : base(source)
	{
		Callback = callback ?? throw new ArgumentNullException(nameof(callback));
	}

	protected override void SourceChanged()
	{
		Callback();
	}
}
