namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/emoji-categories">emoji categories</a>, to be used when selecting custom emojis to set as <a href="https://corefork.telegram.org/api/files#sticker-profile-pictures">profile picture</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getEmojiProfilePhotoGroups"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetEmojiProfilePhotoGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups, MyTelegram.Schema.Messages.IEmojiGroups>
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups obj)
    {
        return Task.FromResult<IEmojiGroups>(new TEmojiGroups { Groups = [] });
    }
}