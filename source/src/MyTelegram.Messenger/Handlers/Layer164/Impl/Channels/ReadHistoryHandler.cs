// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> history as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReadHistory, IBool>,
    Channels.IReadHistoryHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;
    public ReadHistoryHandler(ICommandBus commandBus,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReadHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var messageReadModel =
                await _queryProcessor.ProcessAsync(
                    new GetMessageByIdQuery(MessageId.Create(inputChannel.ChannelId, obj.MaxId).Value), default);

            if (messageReadModel == null)
            {
                RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
            }

            var command = new ReadChannelInboxMessageCommand(
                DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                input.ToRequestInfo(),
                input.UserId,
                inputChannel.ChannelId,
                obj.MaxId,
                messageReadModel!.SenderPeerId,
                null);
            await _commandBus.PublishAsync(command, default);
            return null!;
        }

        throw new NotSupportedException("not supported peer,read channel history only support channel");
    }
}
