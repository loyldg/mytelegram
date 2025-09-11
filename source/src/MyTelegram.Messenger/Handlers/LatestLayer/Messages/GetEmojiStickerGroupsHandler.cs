namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/emoji-categories">emoji categories</a>, to be used when choosing a sticker.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiStickerGroups" />
///</summary>
internal sealed class GetEmojiStickerGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiStickerGroups, MyTelegram.Schema.Messages.IEmojiGroups>
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiStickerGroups obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IEmojiGroups>(new TEmojiGroups
        {
            Groups = []
        });
    }
}
