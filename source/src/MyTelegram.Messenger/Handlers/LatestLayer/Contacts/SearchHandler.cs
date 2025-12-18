using RequestSearch = MyTelegram.Schema.Contacts.RequestSearch;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Returns users found by username substring.
/// Possible errors
/// Code Type Description
/// 400 QUERY_TOO_SHORT The query string is too short.
/// 400 SEARCH_QUERY_EMPTY The search query is empty.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.search"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchHandler(IContactAppService contactAppService, ISearchConverterService searchConverterService) : RpcResultObjectHandler<RequestSearch, Schema.Contacts.IFound>
{
    protected override async Task<IFound> HandleCoreAsync(IRequestInput input, RequestSearch obj)
    {
        var userId = input.UserId;
        var searchResult = await contactAppService.SearchAsync(userId, obj.Q, obj.Limit);
        return searchConverterService.ToFound(input, searchResult, input.Layer);
    }
}