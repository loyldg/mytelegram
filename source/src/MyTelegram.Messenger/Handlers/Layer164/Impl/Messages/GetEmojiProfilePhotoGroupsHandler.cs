// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji categories</a>, to be used when selecting custom emojis to set as <a href="https://corefork.telegram.org/api/files#sticker-profile-pictures">profile picture</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiProfilePhotoGroups" />
///</summary>
internal sealed class GetEmojiProfilePhotoGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiProfilePhotoGroupsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiProfilePhotoGroups obj)
    {
        return Task.FromResult<IEmojiGroups>(new TEmojiGroups
        {
            Groups = new()
        });
    }
}
