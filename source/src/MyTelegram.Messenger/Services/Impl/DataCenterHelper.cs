using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.MessengerServer;

namespace MyTelegram.Messenger.Services.Impl;

public class DataCenterHelper : IDataCenterHelper //, ISingletonDependency
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public DataCenterHelper(IOptions<MyTelegramMessengerServerOptions> options)
    {
        _options = options;
    }

    public int GetMediaDcId()
    {
        //var dcId=_options.Value.IsMediaDc
        var defaultDcId = MyTelegramServerDomainConsts.MediaDcId;
        var dc = _options.Value.DcOptions?.FirstOrDefault(p => p.Id == defaultDcId);
        if (dc != null)
        {
            return defaultDcId;
        }

        return _options.Value.ThisDcId;
    }
}