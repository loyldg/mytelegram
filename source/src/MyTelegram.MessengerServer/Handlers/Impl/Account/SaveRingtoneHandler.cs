// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class SaveRingtoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveRingtone, MyTelegram.Schema.Account.ISavedRingtone>,
    Account.ISaveRingtoneHandler
{
    protected override Task<MyTelegram.Schema.Account.ISavedRingtone> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveRingtone obj)
    {
        throw new NotImplementedException();
    }
}
