// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Dismiss new pending peers recently added to a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/method/chatlists.hideChatlistUpdates" />
///</summary>
internal sealed class HideChatlistUpdatesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestHideChatlistUpdates, IBool>,
    Chatlists.IHideChatlistUpdatesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestHideChatlistUpdates obj)
    {
        throw new NotImplementedException();
    }
}
