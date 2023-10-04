using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class GetContactSignUpNotificationHandler : RpcResultObjectHandler<RequestGetContactSignUpNotification, IBool>,
    IGetContactSignUpNotificationHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;

    public GetContactSignUpNotificationHandler(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestGetContactSignUpNotification obj)
    {
        var user = await _queryProcessor
                .ProcessAsync(new GetUserByIdQuery(input.UserId), CancellationToken.None)
            ;

        return user!.ShowContactSignUpNotification ? new TBoolTrue() : new TBoolFalse();
    }
}