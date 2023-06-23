// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class GetRecentEmojiStatusesHandler : RpcResultObjectHandler<Schema.Account.RequestGetRecentEmojiStatuses,
        Schema.Account.IEmojiStatuses>,
    Account.IGetRecentEmojiStatusesHandler, IProcessedHandler
{
    protected override Task<Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestGetRecentEmojiStatuses obj)
    {
        return Task.FromResult<Schema.Account.IEmojiStatuses>(new TEmojiStatuses
        {
            Statuses = new()
        });
    }
}