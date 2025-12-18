namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Upload notification sound, use <a href="https://corefork.telegram.org/method/account.saveRingtone">account.saveRingtone</a> to convert it and add it to the list of saved notification sounds.
/// Possible errors
/// Code Type Description
/// 400 RINGTONE_MIME_INVALID The MIME type for the ringtone is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.uploadRingtone"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UploadRingtoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUploadRingtone, MyTelegram.Schema.IDocument>
{
    protected override Task<MyTelegram.Schema.IDocument> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestUploadRingtone obj)
    {
        throw new NotImplementedException();
    }
}