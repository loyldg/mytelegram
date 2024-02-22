// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the list of contact statuses.
/// See <a href="https://corefork.telegram.org/method/contacts.getStatuses" />
///</summary>
internal sealed class GetStatusesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetStatuses, TVector<MyTelegram.Schema.IContactStatus>>,
    Contacts.IGetStatusesHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IUserStatusCacheAppService _userStatusAppService;

    public GetStatusesHandler(IUserStatusCacheAppService userStatusAppService,
        IQueryProcessor queryProcessor)
    {
        _userStatusAppService = userStatusAppService;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<TVector<IContactStatus>> HandleCoreAsync(IRequestInput input,
        RequestGetStatuses obj)
    {
        var contactReadModels = await _queryProcessor.ProcessAsync(new GetContactsByUserIdQuery(input.UserId), default);

        var statusList = new List<IContactStatus>();
        foreach (var contactReadModel in contactReadModels)
        {
            statusList.Add(new TContactStatus {
                Status = _userStatusAppService.GetUserStatus(contactReadModel.TargetUserId),
                UserId = contactReadModel.TargetUserId
            });
        }

        return new TVector<IContactStatus>(statusList);
    }
}
