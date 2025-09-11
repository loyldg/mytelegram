namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Get a set of suggested <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickers</a> that can be used in an <a href="https://corefork.telegram.org/api/colors">accent color pattern</a>.
/// See <a href="https://corefork.telegram.org/method/account.getDefaultBackgroundEmojis" />
///</summary>
internal sealed class GetDefaultBackgroundEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultBackgroundEmojis, MyTelegram.Schema.IEmojiList>
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultBackgroundEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = []
        });
    }
}
