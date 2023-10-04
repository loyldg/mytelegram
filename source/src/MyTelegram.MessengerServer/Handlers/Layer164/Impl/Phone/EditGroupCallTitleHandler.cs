using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class EditGroupCallTitleHandler : RpcResultObjectHandler<RequestEditGroupCallTitle, IUpdates>,
    IEditGroupCallTitleHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestEditGroupCallTitle obj)
    {
        throw new NotImplementedException();
    }
}