// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.updateColor" />
///</summary>
internal sealed class UpdateColorHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateColor, MyTelegram.Schema.IUpdates>,
    Channels.IUpdateColorHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateColor obj)
    {
        throw new NotImplementedException();
    }
}
