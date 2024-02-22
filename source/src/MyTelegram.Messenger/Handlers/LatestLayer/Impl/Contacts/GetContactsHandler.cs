// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the current user's contact list.
/// See <a href="https://corefork.telegram.org/method/contacts.getContacts" />
///</summary>
internal sealed class GetContactsHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContacts, MyTelegram.Schema.Contacts.IContacts>,
    Contacts.IGetContactsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;

    public GetContactsHandler(IQueryProcessor queryProcessor,
        ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService, IPrivacyAppService privacyAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<IContacts> HandleCoreAsync(IRequestInput input,
        RequestGetContacts obj)
    {
        var contactReadModels = await _queryProcessor
            .ProcessAsync(new GetContactsByUserIdQuery(input.UserId), CancellationToken.None);
        var userIdList = contactReadModels.Select(p => p.TargetUserId).ToList();
        var userReadModels = await _queryProcessor
            .ProcessAsync(new GetUsersByUidListQuery(userIdList), CancellationToken.None);
        var privacies = await _privacyAppService.GetPrivacyListAsync(userIdList);
        var photos = await _photoAppService.GetPhotosAsync(userReadModels,contactReadModels);
        var userList = _layeredUserService.GetConverter(input.Layer).ToUserList(input.UserId, userReadModels, photos, contactReadModels, privacies);

        foreach (var user in userList)
        {
            user.Contact = true;
        }

        var r = new TContacts
        {
            Contacts =
                new TVector<IContact>(contactReadModels.Select(p =>
                    new TContact { UserId = p.TargetUserId, Mutual = false })),
            Users = new TVector<IUser>(userList),
            SavedCount = contactReadModels.Count
        };

        return r;
        //return Task.FromResult<IContacts>(r);
    }
}
