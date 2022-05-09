using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class JoinChannelHandler : RpcResultObjectHandler<RequestJoinChannel, IUpdates>,
    IJoinChannelHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IRandomHelper _randomHelper;

    public JoinChannelHandler(ICommandBus commandBus,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestJoinChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            //var command = new JoinChannelCommand(ChannelMemberId.Create(inputChannel.ChannelId, input.UserId),
            //    input.ReqMsgId,
            //    input.UserId,
            //    inputChannel.ChannelId,Guid.NewGuid());
            var userIdList = new[] { input.UserId };
            var command = new StartInviteToChannelCommand(
                ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                inputChannel.ChannelId,
                input.UserId,
                userIdList,
                new List<long>(),
                CurrentDate,
                _randomHelper.NextLong(),
                new TMessageActionChatAddUser { Users = new TVector<long>(userIdList) }.ToBytes().ToHexString(),
                Guid.NewGuid());

            await _commandBus.PublishAsync(command, default).ConfigureAwait(false);
            return null!;
        }

        throw new NotImplementedException();
    }
}
