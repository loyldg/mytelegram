using MyTelegram.Domain.Aggregates.UserName;
using MyTelegram.Domain.Commands.UserName;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class UpdateUsernameHandler : RpcResultObjectHandler<RequestUpdateUsername, IBool>,
    IUpdateUsernameHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public UpdateUsernameHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestUpdateUsername obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            var command = new SetUserNameCommand(UserNameId.Create(obj.Username),
                input.ReqMsgId,
                input.UserId,
                PeerType.Channel,
                inputChannel.ChannelId,
                obj.Username,
                Guid.NewGuid());
            await _commandBus.PublishAsync(command, default);
            return null!;
        }

        throw new NotImplementedException();
    }
}