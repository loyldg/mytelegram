namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Reorder pinned <a href="https://corefork.telegram.org/api/saved-messages">saved message dialogs »</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.reorderPinnedSavedDialogs"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ReorderPinnedSavedDialogsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReorderPinnedSavedDialogs, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestReorderPinnedSavedDialogs obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}