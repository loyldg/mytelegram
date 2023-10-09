// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji categories</a>, to be used when selecting <a href="https://corefork.telegram.org/api/custom-emoji">custom emojis</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiGroups" />
///</summary>
internal sealed class GetEmojiGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiGroupsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiGroups obj)
    {
        throw new NotImplementedException();
    }
}
