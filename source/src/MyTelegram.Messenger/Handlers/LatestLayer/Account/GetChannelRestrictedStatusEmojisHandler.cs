namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Returns fetch the full list of <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji IDs »</a> that cannot be used in <a href="https://corefork.telegram.org/api/emoji-status">channel emoji statuses »</a>.
/// See <a href="https://corefork.telegram.org/method/account.getChannelRestrictedStatusEmojis" />
///</summary>
internal sealed class GetChannelRestrictedStatusEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChannelRestrictedStatusEmojis, MyTelegram.Schema.IEmojiList>
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetChannelRestrictedStatusEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = [],
        });
    }
}
