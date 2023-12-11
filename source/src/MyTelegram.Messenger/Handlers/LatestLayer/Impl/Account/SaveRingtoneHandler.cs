// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Save or remove saved notification sound.If the notification sound is already in MP3 format, <a href="https://corefork.telegram.org/constructor/account.savedRingtone">account.savedRingtone</a> will be returned.<br>
/// Otherwise, it will be automatically converted and a <a href="https://corefork.telegram.org/constructor/account.savedRingtoneConverted">account.savedRingtoneConverted</a> will be returned, containing a new <a href="https://corefork.telegram.org/constructor/document">document</a> object that should be used to refer to the ringtone from now on (ie when deleting it using the <code>unsave</code> parameter, or when downloading it).
/// See <a href="https://corefork.telegram.org/method/account.saveRingtone" />
///</summary>
internal sealed class SaveRingtoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveRingtone, MyTelegram.Schema.Account.ISavedRingtone>,
    Account.ISaveRingtoneHandler
{
    protected override Task<MyTelegram.Schema.Account.ISavedRingtone> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveRingtone obj)
    {
        throw new NotImplementedException();
    }
}
