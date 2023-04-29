using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class DeleteMessagesHandler : RpcResultObjectHandler<RequestDeleteMessages, IAffectedMessages>,
    IDeleteMessagesHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;

    public DeleteMessagesHandler(
        ICommandBus commandBus,
        IPtsHelper ptsHelper,
        IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _ptsHelper = ptsHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        RequestDeleteMessages obj)
    {
        // todo:set top message id after delete messages
        if (obj.Id.Count > 0)
        {
            var id = obj.Id.First();
            long? chatCreatorId = null;
            if (obj.Revoke)
            {
                var messageReadModel = await _queryProcessor
                    .ProcessAsync(new GetMessageByIdQuery(MessageId.Create(input.UserId, id).Value), default)
                    ;
                if (messageReadModel == null)
                {
                    ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageIdInvalid);
                }
                switch (messageReadModel!.ToPeerType)
                {
                    case PeerType.Chat:
                    {
                        var chatReadModel = await _queryProcessor
                            .ProcessAsync(new GetChatByChatIdQuery(messageReadModel.ToPeerId), default)
                            ;
                        if (chatReadModel == null)
                        {
                            ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.PeerIdInvalid);
                        }
                        var command = new StartDeleteChatMessagesCommand(ChatId.Create(messageReadModel.ToPeerId),
                            input.ToRequestInfo(),
                            obj.Id.ToList(),
                            obj.Revoke,
                            false,
                            Guid.NewGuid());
                        await _commandBus.PublishAsync(command, default);
                    }
                        break;
                    case PeerType.User:
                    {
                        var command = new StartDeleteUserMessagesCommand(
                            DialogId.Create(input.UserId, messageReadModel.ToPeerType, messageReadModel.ToPeerId),
                            input.ToRequestInfo(),
                            obj.Revoke,
                            obj.Id.ToList(),
                            false,
                            Guid.NewGuid());
                        await _commandBus.PublishAsync(command, default);
                    }
                        break;
                }
            }
            else
            {
            var command = new StartDeleteMessagesCommand(MessageId.Create(input.UserId, id),
                input.ToRequestInfo(),
                obj.Revoke,
                obj.Id.ToList(),
                    chatCreatorId,
                Guid.NewGuid());
            await _commandBus.PublishAsync(command, CancellationToken.None);
            }

            return null!;
        }
        var pts = _ptsHelper.GetCachedPts(input.UserId);

        return new TAffectedMessages { Pts = pts, PtsCount = 0 };
    }
}
