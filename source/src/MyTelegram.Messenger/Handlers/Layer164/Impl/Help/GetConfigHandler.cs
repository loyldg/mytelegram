// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// Returns current configuration, including data center configuration.
/// See <a href="https://corefork.telegram.org/method/help.getConfig" />
///</summary>
internal sealed class GetConfigHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetConfig, MyTelegram.Schema.IConfig>,
    Help.IGetConfigHandler
{
    private readonly IDataCenterHelper _dataCenterHelper;

    private readonly MyTelegramMessengerServerOptions _options;
    private readonly ILayeredService<IConfigConverter> _layeredService;

    public GetConfigHandler(IOptions<MyTelegramMessengerServerOptions> optionsAccessor,
        IDataCenterHelper dataCenterHelper,
        ILayeredService<IConfigConverter> layeredService)
    {
        _options = optionsAccessor.Value;
        _dataCenterHelper = dataCenterHelper;
        _layeredService = layeredService;
    }

    protected override Task<IConfig> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetConfig obj)
    {
        //todo: desktop and app returns different config
        var r = _layeredService.GetConverter(input.Layer)
            .ToConfig(_options.DcOptions, _options.ThisDcId, _dataCenterHelper.GetMediaDcId());
        return Task.FromResult(r);
    }
}
