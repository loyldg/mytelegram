using MyTelegram.Handlers.Phone;
using MyTelegram.Schema.Phone;

namespace MyTelegram.MessengerServer.Handlers.Impl.Phone;

public class ExportGroupCallInviteHandler :
    RpcResultObjectHandler<RequestExportGroupCallInvite, IExportedGroupCallInvite>,
    IExportGroupCallInviteHandler
{
    protected override Task<IExportedGroupCallInvite> HandleCoreAsync(IRequestInput input,
        RequestExportGroupCallInvite obj)
    {
        throw new NotImplementedException();
    }
}