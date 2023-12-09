// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.clickSponsoredMessage" />
///</summary>
internal sealed class ClickSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestClickSponsoredMessage, IBool>,
    Channels.IClickSponsoredMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestClickSponsoredMessage obj)
    {
        throw new NotImplementedException();
    }
}
