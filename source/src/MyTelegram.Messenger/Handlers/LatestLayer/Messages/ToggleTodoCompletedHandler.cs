namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.toggleTodoCompleted" />
///</summary>
internal sealed class ToggleTodoCompletedHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleTodoCompleted, MyTelegram.Schema.IUpdates>
{
    protected override async Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleTodoCompleted obj)
    {
        throw new NotImplementedException();
    }
}
