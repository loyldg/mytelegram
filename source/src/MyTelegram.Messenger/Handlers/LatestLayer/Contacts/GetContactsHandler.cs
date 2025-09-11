namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;

///<summary>
/// Returns the current user's contact list.
/// See <a href="https://corefork.telegram.org/method/contacts.getContacts" />
///</summary>
internal sealed class GetContactsHandler(
    IQueryProcessor queryProcessor,
    IUserConverterService userConverterService,
    IPhotoAppService photoAppService,
    IHashCalculator hashCalculator,
    IUserAppService userAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContacts, MyTelegram.Schema.Contacts.IContacts>
{
    protected override async Task<IContacts> HandleCoreAsync(IRequestInput input,
        RequestGetContacts obj)
    {
        var contactReadModels = await queryProcessor
            .ProcessAsync(new GetContactsByUserIdQuery(input.UserId), CancellationToken.None);
        var userIdList = contactReadModels.Select(p => p.TargetUserId).ToList();
        userIdList.Add(input.UserId);
        var userReadModels = await userAppService.GetListAsync(userIdList);
        var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userIdList);
        var photos = await photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
        var userList = userConverterService.ToUserList(input, userReadModels, photos, contactReadModels, privacyReadModels, input.Layer);

        var validUserIds = new List<long>();
        foreach (var user in userList)
        {
            user.Contact = true;
            validUserIds.Add(user.Id);
        }

        var hash = hashCalculator.GetHash(userIdList);

        if (obj.Hash != 0 && obj.Hash == hash)
        {
            return new TContactsNotModified();
        }

        var contacts = new TContacts
        {
            Contacts =
                [.. contactReadModels.Where(p => validUserIds.Contains(p.TargetUserId)).Select(p =>
                    new TContact { UserId = p.TargetUserId, Mutual = false })],
            Users = [.. userList],
            SavedCount = contactReadModels.Count,
        };

        return contacts;
    }
}
