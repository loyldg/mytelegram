using MyTelegram.Handlers.Help;

namespace MyTelegram.MessengerServer.Handlers.Impl.Help;

public class EditUserInfoHandler : RpcResultObjectHandler<RequestEditUserInfo, IUserInfo>,
    IEditUserInfoHandler
{
    protected override Task<IUserInfo> HandleCoreAsync(IRequestInput input,
        RequestEditUserInfo obj)
    {
        throw new NotImplementedException();
    }
}
