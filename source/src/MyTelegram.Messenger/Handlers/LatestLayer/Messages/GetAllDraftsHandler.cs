namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Return all message <a href="https://corefork.telegram.org/api/drafts">drafts</a>.<br/>
/// Returns all the latest <a href="https://corefork.telegram.org/constructor/updateDraftMessage">updateDraftMessage</a> updates related to all chats with drafts.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getAllDrafts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetAllDraftsHandler(IQueryProcessor queryProcessor, IUpdatesConverterService updatesConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAllDrafts, MyTelegram.Schema.IUpdates>
{
    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input, RequestGetAllDrafts obj)
    {
        var draftReadModels = await queryProcessor.ProcessAsync(new GetAllDraftQuery(input.UserId));
        return updatesConverterService.ToDraftsUpdates(draftReadModels, input.Layer);
    }
}