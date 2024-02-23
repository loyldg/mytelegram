// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getChannelRestrictedStatusEmojis" />
///</summary>
internal sealed class GetChannelRestrictedStatusEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChannelRestrictedStatusEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetChannelRestrictedStatusEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetChannelRestrictedStatusEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = new(),
        });
    }
}
