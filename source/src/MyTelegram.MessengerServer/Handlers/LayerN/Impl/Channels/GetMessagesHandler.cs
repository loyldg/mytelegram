// ReSharper disable All

using RequestGetMessages = MyTelegram.Schema.Channels.LayerN.RequestGetMessages;

namespace MyTelegram.Handlers.Channels.LayerN;

///<summary>
/// Get <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MESSAGE_IDS_EMPTY No message ids were provided.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/channels.getMessages" />
///</summary>
internal sealed class GetMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.LayerN.RequestGetMessages, MyTelegram.Schema.Messages.IMessages>,
    Channels.LayerN.IGetMessagesHandler, IProcessedHandler
{
    private readonly IMessageAppService _messageAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public GetMessagesHandler(IMessageAppService messageAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _messageAppService = messageAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.LayerN.RequestGetMessages obj)
    {
        var idList = obj.Id.ToList();

        long channelId;
        if (obj.Channel is TInputChannel inputChannel)
            channelId = inputChannel.ChannelId;
        else
            throw new BadRequestException("Only TInputChannel supported for get messages");

        var dto = await _messageAppService
            .GetMessagesAsync(
                new GetMessagesInput(input.UserId,
                        channelId,
                        idList,
                        new Peer(PeerType.Channel, channelId))
                    { Limit = 50 });

        return _rpcResultProcessor.ToMessages(dto);
    }
}
