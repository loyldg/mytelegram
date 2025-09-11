namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.appendTodoList" />
///</summary>
internal sealed class AppendTodoListHandler(IQueryProcessor queryProcessor, ICommandBus commandBus, IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAppendTodoList, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestAppendTodoList obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
        var peer = obj.Peer.ToPeer(input.UserId);
        var ownerPeerId = peer.PeerId;
        if (peer.PeerType != PeerType.Channel)
        {
            ownerPeerId = input.UserId;
        }
        var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId.Create(ownerPeerId, obj.MsgId).Value));
        if (messageReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        var media = messageReadModel!.Media2;
        if (media is TMessageMediaToDo messageMediaToDo)
        {
            if (messageReadModel.SenderUserId != input.UserId && !messageMediaToDo.Todo.OthersCanAppend)
            {
                RpcErrors.RpcErrors400.MessageNotModified.ThrowRpcError();
            }

            foreach (var todoItem in obj.List)
            {
                todoItem.Id = messageMediaToDo.Todo.List.Count + 1;
                messageMediaToDo.Todo.List.Add(todoItem);
            }
        }

        var command = new EditOutboxMessageCommand(MessageId.Create(ownerPeerId, obj.MsgId), input.ToRequestInfo(),
            obj.MsgId,
            string.Empty,
            null,
            CurrentDate,
            media,
            null,
            null,
            false,
            null
        );
        await commandBus.PublishAsync(command);

        return null!;
    }
}
