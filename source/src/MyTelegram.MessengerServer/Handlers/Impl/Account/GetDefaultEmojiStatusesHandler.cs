// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class GetDefaultEmojiStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetDefaultEmojiStatuses, MyTelegram.Schema.Account.IEmojiStatuses>,
    Account.IGetDefaultEmojiStatusesHandler, IProcessedHandler
{
    protected override Task<MyTelegram.Schema.Account.IEmojiStatuses> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetDefaultEmojiStatuses obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IEmojiStatuses>(new TEmojiStatusesNotModified());
    }
}
