using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetGroupsForDiscussionHandler : RpcResultObjectHandler<RequestGetGroupsForDiscussion, IChats>,
    IGetGroupsForDiscussionHandler, IProcessedHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetGroupsForDiscussionHandler(IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor)
    {
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetGroupsForDiscussion obj)
    {
        var channelReadModels = await _queryProcessor.ProcessAsync(new GetMegaGroupByUidQuery(input.UserId), default)
            .ConfigureAwait(false);

        var channelList = _rpcResultProcessor.ToChannelList(channelReadModels,
            channelReadModels.Select(p => p.ChannelId).ToList(),
            Array.Empty<IChannelMemberReadModel>(),
            input.UserId,
            true);

        return new TChats { Chats = new TVector<IChat>(channelList) };
    }
}
