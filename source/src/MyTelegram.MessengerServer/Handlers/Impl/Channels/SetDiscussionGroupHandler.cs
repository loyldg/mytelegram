using MyTelegram.Domain.Commands.Channel;
using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class SetDiscussionGroupHandler : RpcResultObjectHandler<RequestSetDiscussionGroup, IBool>,
    ISetDiscussionGroupHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;

    public SetDiscussionGroupHandler(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSetDiscussionGroup obj)
    {
        var broadcastChannel = (TInputChannel)obj.Broadcast;
        //var groupChannel = (TInputChannel)input.Request.Group;
        var groupId = 0L;
        switch (obj.Group)
        {
            case TInputChannel inputChannel:
                groupId = inputChannel.ChannelId;
                break;

            case TInputChannelEmpty:
                break;

            case TInputChannelFromMessage:
                break;

            default:
                throw new NotSupportedException();
        }

        var command = new SetDiscussionGroupCommand(ChannelId.Create(broadcastChannel.ChannelId),
            input.ReqMsgId,
            input.UserId,
            broadcastChannel.ChannelId,
            groupId);
        await _commandBus.PublishAsync(command, CancellationToken.None).ConfigureAwait(false);
        return null!;
    }
}
