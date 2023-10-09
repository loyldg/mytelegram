// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get <a href="https://corefork.telegram.org/passport">passport</a> configuration
/// See <a href="https://corefork.telegram.org/method/help.getPassportConfig" />
///</summary>
internal sealed class GetPassportConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPassportConfig, MyTelegram.Schema.Help.IPassportConfig>,
    Help.IGetPassportConfigHandler
{
    protected override Task<MyTelegram.Schema.Help.IPassportConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPassportConfig obj)
    {
        throw new NotImplementedException();
    }
}
