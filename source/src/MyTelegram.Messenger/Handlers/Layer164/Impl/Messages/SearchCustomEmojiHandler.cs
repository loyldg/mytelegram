// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Look for <a href="https://corefork.telegram.org/api/custom-emoji">custom emojis</a> associated to a UTF8 emoji
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_EMPTY The emoji is empty.
/// See <a href="https://corefork.telegram.org/method/messages.searchCustomEmoji" />
///</summary>
internal sealed class SearchCustomEmojiHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchCustomEmoji, MyTelegram.Schema.IEmojiList>,
    Messages.ISearchCustomEmojiHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchCustomEmoji obj)
    {
        throw new NotImplementedException();
    }
}
