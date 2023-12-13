// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getDefaultBackgroundEmojis" />
///</summary>
internal sealed class GetDefaultBackgroundEmojisHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultBackgroundEmojis, MyTelegram.Schema.IEmojiList>,
    Account.IGetDefaultBackgroundEmojisHandler
{
    protected override Task<MyTelegram.Schema.IEmojiList> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultBackgroundEmojis obj)
    {
        return Task.FromResult<MyTelegram.Schema.IEmojiList>(new TEmojiList
        {
            DocumentId = new()
        });
    }
}
