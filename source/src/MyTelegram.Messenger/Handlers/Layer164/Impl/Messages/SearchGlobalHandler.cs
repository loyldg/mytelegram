// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Search for messages and peers globally
/// <para>Possible errors</para>
/// Code Type Description
/// 400 FOLDER_ID_INVALID Invalid folder ID.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// See <a href="https://corefork.telegram.org/method/messages.searchGlobal" />
///</summary>
internal sealed class SearchGlobalHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchGlobal, MyTelegram.Schema.Messages.IMessages>,
    Messages.ISearchGlobalHandler
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSearchGlobal obj)
    {
        throw new NotImplementedException();
    }
}
