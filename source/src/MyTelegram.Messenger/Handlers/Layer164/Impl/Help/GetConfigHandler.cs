// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns current configuration, including data center configuration.
/// See <a href="https://corefork.telegram.org/method/help.getConfig" />
///</summary>
internal sealed class GetConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetConfig, MyTelegram.Schema.IConfig>,
    Help.IGetConfigHandler
{
    protected override Task<MyTelegram.Schema.IConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetConfig obj)
    {
        throw new NotImplementedException();
    }
}
