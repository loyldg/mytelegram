// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// See <a href="https://corefork.telegram.org/method/help.getPeerProfileColors" />
///</summary>
internal sealed class GetPeerProfileColorsHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPeerProfileColors, MyTelegram.Schema.Help.IPeerColors>,
    Help.IGetPeerProfileColorsHandler
{
    protected override Task<MyTelegram.Schema.Help.IPeerColors> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPeerProfileColors obj)
    {
        throw new NotImplementedException();
    }
}
