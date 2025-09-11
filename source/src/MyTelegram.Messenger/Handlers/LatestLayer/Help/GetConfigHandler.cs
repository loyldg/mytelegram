namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

///<summary>
/// Returns current configuration, including data center configuration.
/// See <a href="https://corefork.telegram.org/method/help.getConfig" />
///</summary>
internal sealed class GetConfigHandler(
    IOptions<MyTelegramMessengerServerOptions> optionsAccessor,
    IDataCenterHelper dataCenterHelper,
    IUserAppService userAppService,
    ILayeredService<IConfigConverter> layeredService)
    : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetConfig, MyTelegram.Schema.IConfig>
{
    private readonly MyTelegramMessengerServerOptions _options = optionsAccessor.Value;

    protected override async Task<IConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetConfig obj)
    {
        var config = layeredService.GetConverter(input.Layer)
            .ToConfig(_options.DcOptions.Where(p => p.Enabled).ToList(), _options.ThisDcId, dataCenterHelper.GetMediaDcId());

        return config;
    }
}
