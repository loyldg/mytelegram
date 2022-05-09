namespace MyTelegram.MessengerServer.Handlers.Impl;

public class DestroySessionHandler : BaseObjectHandler<RequestDestroySession, IDestroySessionRes>,
    IDestroySessionHandler, IProcessedHandler
{
    //private readonly ISessionAppService _sessionAppService;

    //public DestroySessionHandler(ISessionAppService sessionAppService)
    //{
    //    _sessionAppService = sessionAppService;
    //}

    protected override Task<IDestroySessionRes> HandleCoreAsync(IRequestInput input,
        RequestDestroySession obj)
    {
        throw new NotImplementedException();
        //_sessionAppService.DestroySession(input.UserId, obj.SessionId);
        //return Task.FromResult<IDestroySessionRes>(new TDestroySessionOk { SessionId = obj.SessionId });
    }
}
