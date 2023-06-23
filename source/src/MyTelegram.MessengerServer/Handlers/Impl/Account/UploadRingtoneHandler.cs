// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class UploadRingtoneHandler : RpcResultObjectHandler<Schema.Account.RequestUploadRingtone, Schema.IDocument>,
    Account.IUploadRingtoneHandler
{
    protected override Task<Schema.IDocument> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestUploadRingtone obj)
    {
        throw new NotImplementedException();
    }
}