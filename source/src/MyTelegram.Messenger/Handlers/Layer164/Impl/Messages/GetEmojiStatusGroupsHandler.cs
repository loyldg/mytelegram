// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Represents a list of <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji categories</a>, to be used when selecting custom emojis to set as <a href="https://corefork.telegram.org/api">custom emoji status</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiStatusGroups" />
///</summary>
internal sealed class GetEmojiStatusGroupsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiStatusGroups, MyTelegram.Schema.Messages.IEmojiGroups>,
    Messages.IGetEmojiStatusGroupsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGroups> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetEmojiStatusGroups obj)
    {
        throw new NotImplementedException();
    }
}
