// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class SaveRingtoneHandler : RpcResultObjectHandler<RequestSaveRingtone, ISavedRingtone>,
    Account.ISaveRingtoneHandler
{
    protected override Task<ISavedRingtone> HandleCoreAsync(IRequestInput input,
        RequestSaveRingtone obj)
    {
        throw new NotImplementedException();
    }
}