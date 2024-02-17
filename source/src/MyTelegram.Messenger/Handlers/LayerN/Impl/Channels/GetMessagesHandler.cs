// ReSharper disable All

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
    Channels.LayerN.IGetMessagesHandler
{
    private readonly IMessageAppService _messageAppService;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ILayeredService<IRpcResultProcessor> _layeredService;

    public GetMessagesHandler(IMessageAppService messageAppService,
        //IRpcResultProcessor rpcResultProcessor,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IRpcResultProcessor> layeredService)
    {
        _messageAppService = messageAppService;
        //_rpcResultProcessor = rpcResultProcessor;
        _accessHashHelper = accessHashHelper;
        _layeredService = layeredService;
    }

    protected override async Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.LayerN.RequestGetMessages obj)
    {
        long channelId = 0;
        if (obj.Channel is TInputChannel inputChannel)
        {
            channelId = inputChannel.ChannelId;
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);
        }
        else
        {
            RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        }
        var idList = obj.Id.ToList();
        var dto = await _messageAppService
            .GetMessagesAsync(
                new GetMessagesInput(input.UserId,
                        channelId,
                        idList,
                        new Peer(PeerType.Channel, channelId))
                { Limit = 50 });

        //return _rpcResultProcessor.ToMessages(dto, input.Layer);
        return _layeredService.GetConverter(input.Layer).ToMessages(dto, input.Layer);
    }
}
