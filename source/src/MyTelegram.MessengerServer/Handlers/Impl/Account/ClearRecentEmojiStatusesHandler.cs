// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class ClearRecentEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses, IBool>,
    Account.IClearRecentEmojiStatusesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestClearRecentEmojiStatuses obj)
    {
        throw new NotImplementedException();
    }
}
