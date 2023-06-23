using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class EditChatAdminHandler : RpcResultObjectHandler<RequestEditChatAdmin, IBool>,
    IEditChatAdminHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestEditChatAdmin obj)
    {
        throw new NotImplementedException();
    }
}