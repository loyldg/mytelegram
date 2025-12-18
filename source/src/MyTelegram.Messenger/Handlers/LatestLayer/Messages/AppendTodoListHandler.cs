namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Appends one or more items to a <a href="https://corefork.telegram.org/api/todo">todo list »</a>.
/// Possible errors
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TODO_ITEM_DUPLICATE Duplicate <a href="https://corefork.telegram.org/api/todo">checklist items</a> detected.
/// 400 TODO_NOT_MODIFIED No todo items were specified, so no changes were made to the todo list.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.appendTodoList"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class AppendTodoListHandler(IQueryProcessor queryProcessor, ICommandBus commandBus, IAccessHashHelper accessHashHelper) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestAppendTodoList, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestAppendTodoList obj)
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

        var command = new EditOutboxMessageCommand(MessageId.Create(ownerPeerId, obj.MsgId), input.ToRequestInfo(), obj.MsgId, string.Empty, CurrentDate, null, media, null, false, null);
        await commandBus.PublishAsync(command);
        return null !;
    }
}