namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get a list of <a href="https://corefork.telegram.org/api/sponsored-messages">sponsored messages for a peer, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/messages.getSponsoredMessages" />
///</summary>
internal sealed class GetSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSponsoredMessages, MyTelegram.Schema.Messages.ISponsoredMessages>
{
    protected override Task<MyTelegram.Schema.Messages.ISponsoredMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetSponsoredMessages obj)
    {
        return Task.FromResult<ISponsoredMessages>(new TSponsoredMessagesEmpty());
    }
}
