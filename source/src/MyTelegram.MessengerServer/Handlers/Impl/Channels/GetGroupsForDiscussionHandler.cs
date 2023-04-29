using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetGroupsForDiscussionHandler : RpcResultObjectHandler<RequestGetGroupsForDiscussion, IChats>,
    IGetGroupsForDiscussionHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IQueryProcessor _queryProcessor;

    public GetGroupsForDiscussionHandler(IQueryProcessor queryProcessor,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
        _chatConverter = chatConverter;
    }

    protected override async Task<IChats> HandleCoreAsync(IRequestInput input,
        RequestGetGroupsForDiscussion obj)
    {
        var channelReadModels = await _queryProcessor.ProcessAsync(new GetMegaGroupByUidQuery(input.UserId), default)
            ;

        var channelList = _chatConverter.ToChannelList(channelReadModels,
            channelReadModels.Select(p => p.ChannelId).ToList(),
            Array.Empty<IChannelMemberReadModel>(),
            input.UserId,
            true);

        return new TChats { Chats = new TVector<IChat>(channelList) };
    }
}
