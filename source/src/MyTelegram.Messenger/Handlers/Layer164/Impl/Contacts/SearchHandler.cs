// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns users found by username substring.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 QUERY_TOO_SHORT The query string is too short.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// See <a href="https://corefork.telegram.org/method/contacts.search" />
///</summary>
internal sealed class SearchHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestSearch, MyTelegram.Schema.Contacts.IFound>,
    Contacts.ISearchHandler
{
    protected override Task<MyTelegram.Schema.Contacts.IFound> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestSearch obj)
    {
        throw new NotImplementedException();
    }
}
