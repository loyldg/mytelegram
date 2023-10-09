// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Modify autosave settings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/account.saveAutoSaveSettings" />
///</summary>
internal sealed class SaveAutoSaveSettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestSaveAutoSaveSettings, IBool>,
    Account.ISaveAutoSaveSettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestSaveAutoSaveSettings obj)
    {
        throw new NotImplementedException();
    }
}
