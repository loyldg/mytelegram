namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Activate or deactivate a purchased <a href="https://fragment.com/">fragment.com</a> username associated to a <a href="https://corefork.telegram.org/api/channel">supergroup or channel</a> we own.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 USERNAMES_ACTIVE_TOO_MUCH The maximum number of active usernames was reached.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// See <a href="https://corefork.telegram.org/method/channels.toggleUsername" />
///</summary>
internal sealed class ToggleUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestToggleUsername, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestToggleUsername obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
