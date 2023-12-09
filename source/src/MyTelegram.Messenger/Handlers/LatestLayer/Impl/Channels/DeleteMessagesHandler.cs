// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete messages in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>,
    Channels.IDeleteMessagesHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteMessagesHandler(ICommandBus commandBus,
        IPtsHelper ptsHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _ptsHelper = ptsHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteMessages obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            if (obj.Id.Count > 0)
            {
                var firstMessageId = obj.Id.First();
                var command = new StartDeleteMessagesCommand(
                    MessageId.Create(inputChannel.ChannelId, firstMessageId), input.ToRequestInfo(),
                    false,
                    obj.Id.ToList(),
                    null,
                    Guid.NewGuid());
                await _commandBus.PublishAsync(command, CancellationToken.None);
                return null!;
            }

            var pts = _ptsHelper.GetCachedPts(input.UserId);

            return new TAffectedMessages { Pts = pts, PtsCount = 0 };
        }

        throw new NotImplementedException();
    }
}
