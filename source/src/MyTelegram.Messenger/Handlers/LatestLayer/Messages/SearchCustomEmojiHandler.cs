namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Look for <a href="https://corefork.telegram.org/api/custom-emoji">custom emojis</a> associated to a UTF8 emoji
/// Possible errors
/// Code Type Description
/// 400 EMOTICON_EMPTY The emoji is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.searchCustomEmoji"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchCustomEmojiHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchCustomEmoji, MyTelegram.Schema.IEmojiList>
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSearchCustomEmoji obj)
    {
        throw new NotImplementedException();
    }
}