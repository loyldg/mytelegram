using MyTelegram.Handlers.Users;
using MyTelegram.Schema.Users;

namespace MyTelegram.MessengerServer.Handlers.Impl.Users;

public class GetUsersHandler : RpcResultObjectHandler<RequestGetUsers, TVector<IUser>>,
    IGetUsersHandler
{
    protected override Task<TVector<IUser>> HandleCoreAsync(IRequestInput input,
        RequestGetUsers obj)
    {
        throw new NotImplementedException();
    }
}
