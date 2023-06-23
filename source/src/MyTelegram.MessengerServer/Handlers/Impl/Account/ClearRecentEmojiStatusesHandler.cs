// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class ClearRecentEmojiStatusesHandler : RpcResultObjectHandler<RequestClearRecentEmojiStatuses, IBool>,
    Account.IClearRecentEmojiStatusesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestClearRecentEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}