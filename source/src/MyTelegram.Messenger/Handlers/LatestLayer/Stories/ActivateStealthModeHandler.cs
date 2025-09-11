namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.activateStealthMode" />
///</summary>
internal sealed class ActivateStealthModeHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestActivateStealthMode, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestActivateStealthMode obj)
    {
        throw new NotImplementedException();
    }
}
