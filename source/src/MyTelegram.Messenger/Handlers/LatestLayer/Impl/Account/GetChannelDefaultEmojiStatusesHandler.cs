// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getChannelDefaultEmojiStatuses" />
///</summary>
internal sealed class GetChannelDefaultEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetChannelDefaultEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetChannelDefaultEmojiStatusesHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetChannelDefaultEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}
