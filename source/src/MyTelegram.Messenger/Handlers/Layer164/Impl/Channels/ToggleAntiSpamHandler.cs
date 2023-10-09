// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Enable or disable the <a href="https://corefork.telegram.org/api/antispam">native antispam system</a>.
/// See <a href="https://corefork.telegram.org/method/channels.toggleAntiSpam" />
///</summary>
internal sealed class ToggleAntiSpamHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleAntiSpam, MyTelegram.Schema.IUpdates>,
    Channels.IToggleAntiSpamHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleAntiSpam obj)
    {
        throw new NotImplementedException();
    }
}
