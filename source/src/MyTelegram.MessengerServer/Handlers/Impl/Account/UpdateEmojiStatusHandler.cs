// ReSharper disable All

using MyTelegram.Schema.Account;

namespace MyTelegram.Handlers.Account;

public class UpdateEmojiStatusHandler : RpcResultObjectHandler<RequestUpdateEmojiStatus, IBool>,
    Account.IUpdateEmojiStatusHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateEmojiStatus obj)
    {
        throw new NotImplementedException();
    }
}
