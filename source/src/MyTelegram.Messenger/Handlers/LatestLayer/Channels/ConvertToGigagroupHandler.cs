namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Convert a <a href="https://corefork.telegram.org/api/channel">supergroup</a> to a <a href="https://corefork.telegram.org/api/channel">gigagroup</a>, when requested by <a href="https://corefork.telegram.org/api/config#channel-suggestions">channel suggestions</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_ID_INVALID The specified supergroup ID is invalid.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FORUM_ENABLED You can't execute the specified action because the group is a <a href="https://corefork.telegram.org/api/forum">forum</a>, disable forum functionality to continue.
/// 400 PARTICIPANTS_TOO_FEW Not enough participants.
/// See <a href="https://corefork.telegram.org/method/channels.convertToGigagroup" />
///</summary>
internal sealed class ConvertToGigagroupHandler : RpcResultObjectHandler<RequestConvertToGigagroup, IUpdates>
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestConvertToGigagroup obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = [],
            Chats = [],
            Users = [],
            Date = CurrentDate
        });
    }
}
