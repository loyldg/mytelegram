namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Use this method to obtain the online statuses of all contacts with an accessible Telegram account.
/// See <a href="https://corefork.telegram.org/method/contacts.getStatuses" />
///</summary>
internal sealed class GetStatusesHandler(
    IUserStatusCacheAppService userStatusAppService,
    IQueryProcessor queryProcessor)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetStatuses, TVector<MyTelegram.Schema.IContactStatus>>
{
    protected override async Task<TVector<IContactStatus>> HandleCoreAsync(IRequestInput input,
        RequestGetStatuses obj)
    {
        var contactReadModels = await queryProcessor.ProcessAsync(new GetContactsByUserIdQuery(input.UserId), default);

        var statusList = new List<IContactStatus>();
        foreach (var contactReadModel in contactReadModels)
        {
            statusList.Add(new TContactStatus {
                Status = userStatusAppService.GetUserStatus(contactReadModel.TargetUserId),
                UserId = contactReadModel.TargetUserId
            });
        }

        return [.. statusList];
    }
}
