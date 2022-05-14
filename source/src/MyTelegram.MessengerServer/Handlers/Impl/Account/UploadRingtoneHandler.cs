// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class UploadRingtoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadRingtone, MyTelegram.Schema.IDocument>,
    Account.IUploadRingtoneHandler
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUploadRingtone obj)
    {
        throw new NotImplementedException();
    }
}
