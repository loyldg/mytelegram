namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Mark one or more items of a <a href="https://corefork.telegram.org/api/todo">todo list »</a> as completed or not completed.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.toggleTodoCompleted"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleTodoCompletedHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleTodoCompleted, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestToggleTodoCompleted obj)
    {
        throw new NotImplementedException();
    }
}