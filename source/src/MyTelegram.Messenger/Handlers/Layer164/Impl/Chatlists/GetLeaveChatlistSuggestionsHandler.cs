// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Returns identifiers of pinned or always included chats from a chat folder imported using a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>, which are suggested to be left when the chat folder is deleted.
/// See <a href="https://corefork.telegram.org/method/chatlists.getLeaveChatlistSuggestions" />
///</summary>
internal sealed class GetLeaveChatlistSuggestionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestGetLeaveChatlistSuggestions, TVector<MyTelegram.Schema.IPeer>>,
    Chatlists.IGetLeaveChatlistSuggestionsHandler
{
    protected override Task<TVector<MyTelegram.Schema.IPeer>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestGetLeaveChatlistSuggestions obj)
    {
        throw new NotImplementedException();
    }
}
