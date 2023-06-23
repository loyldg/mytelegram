using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class EditCreatorHandler : RpcResultObjectHandler<RequestEditCreator, IUpdates>,
    IEditCreatorHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditCreator obj)
    {
        throw new NotImplementedException();
    }
}