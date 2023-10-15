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
    private readonly IContactAppService _contactAppService;
    private readonly IRpcResultProcessor _rpcResultProcessor;

    public SearchHandler(IContactAppService contactAppService,
        IRpcResultProcessor rpcResultProcessor)
    {
        _contactAppService = contactAppService;
        _rpcResultProcessor = rpcResultProcessor;
    }

    protected override async Task<IFound> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestSearch obj)
    {
        var userId = input.UserId;
        var r = await _contactAppService.SearchAsync(userId, obj.Q);

        return _rpcResultProcessor.ToFound(r, input.Layer);
    }
}
