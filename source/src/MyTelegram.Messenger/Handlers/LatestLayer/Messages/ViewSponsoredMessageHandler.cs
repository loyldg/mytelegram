namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Mark a specific <a href="https://corefork.telegram.org/api/sponsored-messages">sponsored message »</a> as read
/// See <a href="https://corefork.telegram.org/method/messages.viewSponsoredMessage" />
///</summary>
internal sealed class ViewSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestViewSponsoredMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestViewSponsoredMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
