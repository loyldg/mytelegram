// ReSharper disable All

namespace MyTelegram.Handlers.Account;

public class UpdateEmojiStatusHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateEmojiStatus, IBool>,
    Account.IUpdateEmojiStatusHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateEmojiStatus obj)
    {
        throw new NotImplementedException();
    }
}
