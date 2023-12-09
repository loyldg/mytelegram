// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Upload notification sound, use <a href="https://corefork.telegram.org/method/account.saveRingtone">account.saveRingtone</a> to convert it and add it to the list of saved notification sounds.
/// See <a href="https://corefork.telegram.org/method/account.uploadRingtone" />
///</summary>
internal sealed class UploadRingtoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadRingtone, MyTelegram.Schema.IDocument>,
    Account.IUploadRingtoneHandler
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUploadRingtone obj)
    {
        throw new NotImplementedException();
    }
}
