using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class InviteToChannelHandler : RpcResultObjectHandler<RequestInviteToChannel, IUpdates>,
    IInviteToChannelHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IRandomHelper _randomHelper;

    public InviteToChannelHandler(ICommandBus commandBus,
        IRandomHelper randomHelper,
        IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _randomHelper = randomHelper;
        _peerHelper = peerHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestInviteToChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            //if (obj.Users.Count != 1)
            //{
            //    Logger.LogError($"Only one user can invite to channel per operation.");
            //    throw new BadRequestException("Only one user can invite to channel per operation.");
            //}
            var userIdList = new List<long>();
            var botList = new List<long>();
            foreach (var inputUser in obj.Users)
            {
                if (inputUser is TInputUser tInputUser)
                {
                    userIdList.Add(tInputUser.UserId);
                    if (_peerHelper.IsBotUser(tInputUser.UserId))
                    {
                        botList.Add(tInputUser.UserId);
                    }
                }
            }

            //var userId = userIdList[0];
            var command = new StartInviteToChannelCommand(
                ChannelId.Create(inputChannel.ChannelId),
                input.ToRequestInfo(),
                inputChannel.ChannelId,
                input.UserId,
                userIdList,
                botList,
                CurrentDate,
                _randomHelper.NextLong(),
                new TMessageActionChatAddUser { Users = new TVector<long>(userIdList) }.ToBytes().ToHexString(),
                Guid.NewGuid());
            await _commandBus.PublishAsync(command, CancellationToken.None);

            return null!;
        }

        throw new NotImplementedException();
    }
}
