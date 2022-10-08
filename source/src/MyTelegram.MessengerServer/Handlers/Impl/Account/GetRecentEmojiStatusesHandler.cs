// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class GetRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetRecentEmojiStatusesHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}
