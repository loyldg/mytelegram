namespace MyTelegram.Messenger.Services.Impl;

public class DataCenterHelper : IDataCenterHelper
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public DataCenterHelper(IOptions<MyTelegramMessengerServerOptions> options)
    {
        _options = options;
    }

    public int GetMediaDcId()
    {
        var defaultDcId = MyTelegramServerDomainConsts.MediaDcId;
        var dc = _options.Value.DcOptions?.FirstOrDefault(p => p.Id == defaultDcId);
        if (dc != null)
        {
            return defaultDcId;
        }

        return _options.Value.ThisDcId;
    }
}