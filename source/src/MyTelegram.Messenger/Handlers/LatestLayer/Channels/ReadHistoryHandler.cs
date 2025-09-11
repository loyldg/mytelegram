namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;

///<summary>
/// Mark <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> history as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler(
    ICommandBus commandBus,
    IAccessHashHelper accessHashHelper,
    IQueryProcessor queryProcessor)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReadHistory, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReadHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(input, inputChannel.ChannelId, inputChannel.AccessHash, AccessHashType.Channel);

            var messageReadModel =
                await queryProcessor.ProcessAsync(
                    new GetMessageByIdQuery(MessageId.Create(inputChannel.ChannelId, obj.MaxId).Value));
            
            if (messageReadModel == null)
            {
                RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
            }

            var dialogReadModel = await queryProcessor.ProcessAsync(
                new GetDialogByIdQuery(DialogId.Create(input.UserId, inputChannel.ChannelId.ToChannelPeer()).Value));
            if (dialogReadModel == null)
            {
                return new TBoolTrue();
            }

            if (dialogReadModel!.ReadInboxMaxId >= obj.MaxId)
            {
                return new TBoolFalse();
            }

            var command = new UpdateReadChannelInboxCommand(
                DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId),
                input.ToRequestInfo(),
                messageReadModel!.SenderUserId,
                obj.MaxId
            );
            await commandBus.PublishAsync(command);
            return null!;
        }

        return new TBoolTrue();
    }
}
