// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class GetDefaultEmojiStatusesHandler : RpcResultObjectHandler<Schema.Account.RequestGetDefaultEmojiStatuses,
        Schema.Account.IEmojiStatuses>,
    Account.IGetDefaultEmojiStatusesHandler, IProcessedHandler
{
    protected override Task<Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        Schema.Account.RequestGetDefaultEmojiStatuses obj)
    {
        return Task.FromResult<Schema.Account.IEmojiStatuses>(new TEmojiStatusesNotModified());
    }
}