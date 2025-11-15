namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Set an <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a> for a channel or supergroup.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.updateEmojiStatus"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateEmojiStatus, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestUpdateEmojiStatus obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}