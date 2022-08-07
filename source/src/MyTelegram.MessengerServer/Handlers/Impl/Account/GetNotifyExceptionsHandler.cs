using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetNotifyExceptionsHandler : RpcResultObjectHandler<RequestGetNotifyExceptions, IUpdates>,
    IGetNotifyExceptionsHandler, IProcessedHandler
{
    protected override Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestGetNotifyExceptions obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates
        {
            Updates = new TVector<IUpdate>(),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = CurrentDate
        });
    }
}
