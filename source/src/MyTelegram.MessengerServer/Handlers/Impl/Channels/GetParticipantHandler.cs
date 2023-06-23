using MyTelegram.Handlers.Channels;
using MyTelegram.Schema.Channels;
using IChannelParticipant = MyTelegram.Schema.Channels.IChannelParticipant;

namespace MyTelegram.MessengerServer.Handlers.Impl.Channels;

public class GetParticipantHandler : RpcResultObjectHandler<RequestGetParticipant, IChannelParticipant>,
    IGetParticipantHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetParticipantHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _chatConverter = chatConverter;
    }

    protected override async Task<IChannelParticipant> HandleCoreAsync(IRequestInput input,
        RequestGetParticipant obj)
    {
        var peer = _peerHelper.GetPeer(obj.Participant, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channelMemberReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, peer.PeerId), default)
                ;
            if (channelMemberReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_NOT_PARTICIPANT");

            var userReadModel = await _queryProcessor
                    .ProcessAsync(new GetUserByIdQuery(channelMemberReadModel?.UserId ?? input.UserId), default)
                ;

            if (userReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");

            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);
            //var contactReadModel = await QueryProcessor
            //    .ProcessAsync(new GetContactQuery(input.UserId, peer.PeerId), default);

            var r = _chatConverter.ToChannelParticipant(channelReadModel,
                channelMemberReadModel!,
                userReadModel!,
                input.UserId);
            // Console.WriteLine($"{JsonConvert.SerializeObject(r)}");
            return r;
        }

        throw new NotImplementedException();
    }
}

public class GetParticipantHandlerLayerN :
    RpcResultObjectHandler<Schema.LayerN.RequestGetParticipant, Schema.LayerN.IChannelParticipant>,
    IGetParticipantHandlerLayerN, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetParticipantHandlerLayerN(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
        _peerHelper = peerHelper;
        _chatConverter = chatConverter;
    }

    protected override async Task<Schema.LayerN.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        Schema.LayerN.RequestGetParticipant obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            var channelMemberReadModel = await _queryProcessor
                    .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, peer.PeerId), default)
                ;

            if (channelMemberReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_NOT_PARTICIPANT");

            var userReadModel = await _queryProcessor
                    .ProcessAsync(new GetUserByIdQuery(channelMemberReadModel!.UserId), default)
                ;

            if (userReadModel == null) ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");

            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId), default);

            return _chatConverter.ToChannelParticipantLayerN(channelReadModel,
                channelMemberReadModel,
                userReadModel!,
                input.UserId);
        }

        throw new NotImplementedException();
    }
}