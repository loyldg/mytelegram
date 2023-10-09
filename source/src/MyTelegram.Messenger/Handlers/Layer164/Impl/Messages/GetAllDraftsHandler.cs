// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Return all message <a href="https://corefork.telegram.org/api/drafts">drafts</a>.<br>
/// Returns all the latest <a href="https://corefork.telegram.org/constructor/updateDraftMessage">updateDraftMessage</a> updates related to all chats with drafts.
/// See <a href="https://corefork.telegram.org/method/messages.getAllDrafts" />
///</summary>
internal sealed class GetAllDraftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAllDrafts, MyTelegram.Schema.IUpdates>,
    Messages.IGetAllDraftsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAllDrafts obj)
    {
        throw new NotImplementedException();
    }
}
