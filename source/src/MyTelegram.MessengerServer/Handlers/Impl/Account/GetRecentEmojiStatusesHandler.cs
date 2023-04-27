// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class GetRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetRecentEmojiStatusesHandler,IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        return Task.FromResult<Schema.Account.IEmojiStatuses>(new TEmojiStatuses
        {
            Statuses = new()
        });
    }
}
