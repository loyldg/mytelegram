namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

/// <summary>
///     Get app-specific configuration, see
///     <a href="https://corefork.telegram.org/api/config#client-configuration">client configuration</a> for more info on
///     the result.
///     See <a href="https://corefork.telegram.org/method/help.getAppConfig" />
/// </summary>
internal sealed class GetAppConfigHandler(IAppConfigHelper appConfigHelper) :
    RpcResultObjectHandler<Schema.Help.RequestGetAppConfig, Schema.Help.IAppConfig>
{
    protected override Task<Schema.Help.IAppConfig> HandleCoreAsync(IRequestInput input,
        Schema.Help.RequestGetAppConfig obj)
    {
        var hash = appConfigHelper.GetAppConfigHash();
        if (obj.Hash == hash)
        {
            return Task.FromResult<Schema.Help.IAppConfig>(new TAppConfigNotModified());
        }
        var config = appConfigHelper.GetAppConfig();

        var appConfig = new TAppConfig
        {
            Config = config,
            Hash = hash
        };

        return Task.FromResult<IAppConfig>(appConfig);
    }
}