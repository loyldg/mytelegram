namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Clear all <a href="https://corefork.telegram.org/api/drafts">drafts</a>.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.clearAllDrafts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ClearAllDraftsHandler(IQueryProcessor queryProcessor, ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearAllDrafts, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestClearAllDrafts obj)
    {
        var drafts = await queryProcessor.ProcessAsync(new GetAllDraftQuery(input.UserId));
        foreach (var draftReadModel in drafts)
        {
            var command = new DeleteDraftCommand(TempId.New, draftReadModel.OwnerPeerId, draftReadModel.Peer);
            await commandBus.PublishAsync(command);
        }

        return new TBoolTrue();
    }
}