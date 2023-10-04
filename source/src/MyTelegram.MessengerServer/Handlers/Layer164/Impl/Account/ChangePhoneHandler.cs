using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

// ReSharper disable once UnusedMember.Global
public class ChangePhoneHandler : RpcResultObjectHandler<RequestChangePhone, IUser>,
    IChangePhoneHandler
{
    protected override Task<IUser> HandleCoreAsync(IRequestInput input,
        RequestChangePhone obj)
    {
        throw new NotImplementedException();
    }
}