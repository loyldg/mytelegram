namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/emoji-categories">emoji categories</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiGroups" />
///</summary>
internal sealed class GetEmojiGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiGroups, MyTelegram.Schema.Messages.IEmojiGroups>
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiGroups obj)
    {
        return Task.FromResult<IEmojiGroups>(new TEmojiGroups
        {
            Groups = []
        });
    }
}
