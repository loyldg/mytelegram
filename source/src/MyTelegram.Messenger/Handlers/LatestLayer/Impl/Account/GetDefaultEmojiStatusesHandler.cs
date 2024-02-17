// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// Get a list of default suggested <a href="https://corefork.telegram.org/api/emoji-status">emoji statuses</a>
/// See <a href="https://corefork.telegram.org/method/account.getDefaultEmojiStatuses" />
///</summary>
internal sealed class GetDefaultEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetDefaultEmojiStatusesHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultEmojiStatuses obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IEmojiStatuses>(new TEmojiStatuses
        {
            Statuses = new()
        });
    }
}
