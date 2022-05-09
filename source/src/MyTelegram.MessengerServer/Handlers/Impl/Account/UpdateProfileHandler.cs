using MyTelegram.Domain.Commands.User;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdateProfileHandler : RpcResultObjectHandler<RequestUpdateProfile, IUser>,
    IUpdateProfileHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public UpdateProfileHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        RequestUpdateProfile obj)
    {
        var userId = UserId.Create(input.UserId);
        var command = new UpdateProfileCommand(userId,
            input.ReqMsgId,
            obj.FirstName,
            obj.LastName,
            obj.About);
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);

        return null!;
    }
}
