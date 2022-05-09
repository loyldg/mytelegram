using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetAdminsWithInvitesHandler : RpcResultObjectHandler<RequestGetAdminsWithInvites, IChatAdminsWithInvites>,
    IGetAdminsWithInvitesHandler
{
    protected override Task<IChatAdminsWithInvites> HandleCoreAsync(IRequestInput input,
        RequestGetAdminsWithInvites obj)
    {
        throw new NotImplementedException();
    }
}
