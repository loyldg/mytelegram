using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class SetEncryptedTypingHandler : RpcResultObjectHandler<RequestSetEncryptedTyping, IBool>,
    ISetEncryptedTypingHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetEncryptedTyping obj)
    {
        throw new NotImplementedException();
    }
}