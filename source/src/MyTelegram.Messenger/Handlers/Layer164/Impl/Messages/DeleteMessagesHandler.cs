// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Deletes messages by their identifiers.
/// <para>Possible errors</para>
/// Code Type Description
/// 403 MESSAGE_DELETE_FORBIDDEN You can't delete one of the messages you tried to delete, most likely because it is a service message.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.deleteMessages" />
///</summary>
internal sealed class DeleteMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestDeleteMessages, MyTelegram.Schema.Messages.IAffectedMessages>,
    Messages.IDeleteMessagesHandler
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
        MyTelegram.Schema.Messages.RequestDeleteMessages obj)
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
                    RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
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
                                RpcErrors.RpcErrors400.PeerIdInvalid.ThrowRpcError();
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
                    //0,
                    //0,
                    //null,
                    Guid.NewGuid());
                await _commandBus.PublishAsync(command, CancellationToken.None);
            }

            return null!;
        }

        var pts = _ptsHelper.GetCachedPts(input.UserId);

        return new TAffectedMessages { Pts = pts, PtsCount = 0 };
    }
}
