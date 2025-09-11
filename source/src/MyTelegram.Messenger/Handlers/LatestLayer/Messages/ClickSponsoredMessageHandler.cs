namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Informs the server that the user has interacted with a sponsored message in <a href="https://corefork.telegram.org/api/sponsored-messages#clicking-on-sponsored-messages">one of the ways listed here »</a>.
/// See <a href="https://corefork.telegram.org/method/messages.clickSponsoredMessage" />
///</summary>
internal sealed class ClickSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClickSponsoredMessage, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestClickSponsoredMessage obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
