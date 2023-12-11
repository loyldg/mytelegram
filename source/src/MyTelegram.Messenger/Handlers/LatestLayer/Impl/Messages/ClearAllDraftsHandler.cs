// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Clear all <a href="https://corefork.telegram.org/api/drafts">drafts</a>.
/// See <a href="https://corefork.telegram.org/method/messages.clearAllDrafts" />
///</summary>
internal sealed class ClearAllDraftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestClearAllDrafts, IBool>,
    Messages.IClearAllDraftsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestClearAllDrafts obj)
    {
        throw new NotImplementedException();
    }
}
