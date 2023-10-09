// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get app-specific configuration, see <a href="https://corefork.telegram.org/api/config#client-configuration">client configuration</a> for more info on the result.
/// See <a href="https://corefork.telegram.org/method/help.getAppConfig" />
///</summary>
internal sealed class GetAppConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetAppConfig, MyTelegram.Schema.Help.IAppConfig>,
    Help.IGetAppConfigHandler
{
    protected override Task<MyTelegram.Schema.Help.IAppConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetAppConfig obj)
    {
        throw new NotImplementedException();
    }
}
