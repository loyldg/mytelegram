using MyTelegram.Domain.Aggregates.UserName;
using MyTelegram.Domain.Commands.UserName;
using MyTelegram.Handlers.Account;
using MyTelegram.Schema.Account;

namespace MyTelegram.MessengerServer.Handlers.Impl.Account;

public class UpdateUsernameHandler : RpcResultObjectHandler<RequestUpdateUsername, IUser>,
    IUpdateUsernameHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly ICuckooFilter _cuckooFilter;

    public UpdateUsernameHandler(ICommandBus commandBus,
        ICuckooFilter cuckooFilter)
    {
        _commandBus = commandBus;
        _cuckooFilter = cuckooFilter;
    }

    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        RequestUpdateUsername obj)
    {
        if (await _cuckooFilter
                .ExistsAsync(Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{obj.Username}"))
                .ConfigureAwait(false))
        {
            ThrowHelper.ThrowUserFriendlyException("USERNAME_OCCUPIED");
        }

        var command = new SetUserNameCommand(UserNameId.Create(obj.Username),
            input.ReqMsgId,
            input.UserId,
            PeerType.User,
            input.UserId,
            obj.Username,
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
        return null!;
    }
}
