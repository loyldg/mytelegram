// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Edits notification settings from a given user/group, from all users/all groups.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SETTINGS_INVALID Invalid settings were provided.
/// See <a href="https://corefork.telegram.org/method/account.updateNotifySettings" />
///</summary>
internal sealed class UpdateNotifySettingsHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateNotifySettings, IBool>,
    Account.IUpdateNotifySettingsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateNotifySettings obj)
    {
        throw new NotImplementedException();
    }
}
