// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Activate or deactivate a purchased <a href="https://fragment.com/">fragment.com</a> username associated to a <a href="https://corefork.telegram.org/api/channel">supergroup or channel</a> we own.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 USERNAMES_ACTIVE_TOO_MUCH The maximum number of active usernames was reached.
/// 400 USERNAME_INVALID The provided username is not valid.
/// See <a href="https://corefork.telegram.org/method/channels.toggleUsername" />
///</summary>
internal sealed class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleUsername, IBool>,
    Channels.IToggleUsernameHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleUsername obj)
    {
        throw new NotImplementedException();
    }
}
