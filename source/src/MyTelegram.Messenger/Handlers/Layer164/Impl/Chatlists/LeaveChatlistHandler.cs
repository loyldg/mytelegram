// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Delete a folder imported using a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>
/// See <a href="https://corefork.telegram.org/method/chatlists.leaveChatlist" />
///</summary>
internal sealed class LeaveChatlistHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestLeaveChatlist, MyTelegram.Schema.IUpdates>,
    Chatlists.ILeaveChatlistHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestLeaveChatlist obj)
    {
        throw new NotImplementedException();
    }
}
