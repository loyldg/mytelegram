// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Resolve a phone number to get user info, if their privacy settings allow it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PHONE_NOT_OCCUPIED No user is associated to the specified phone number.
/// See <a href="https://corefork.telegram.org/method/contacts.resolvePhone" />
///</summary>
internal sealed class ResolvePhoneHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolvePhone, MyTelegram.Schema.Contacts.IResolvedPeer>,
    Contacts.IResolvePhoneHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPrivacyAppService _privacyAppService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IPhotoAppService _photoAppService;
    private readonly IContactAppService _contactAppService;
    public ResolvePhoneHandler(IQueryProcessor queryProcessor, IPrivacyAppService privacyAppService, ILayeredService<IUserConverter> layeredUserService, IPhotoAppService photoAppService, IContactAppService contactAppService)
    {
        _queryProcessor = queryProcessor;
        _privacyAppService = privacyAppService;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _contactAppService = contactAppService;
    }

    protected async override Task<MyTelegram.Schema.Contacts.IResolvedPeer> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResolvePhone obj)
    {
        var userReadModel = await _queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(obj.Phone));
        if (userReadModel == null)
        {
            RpcErrors.RpcErrors400.PhoneNotOccupied.ThrowRpcError();
        }

        await _privacyAppService.ApplyPrivacyAsync(input.UserId, userReadModel!.UserId, () =>
         {
             RpcErrors.RpcErrors400.PhoneNotOccupied.ThrowRpcError();
         }, new List<PrivacyType>
         {
            PrivacyType.AddedByPhone
         });

        var privacyList = await _privacyAppService.GetPrivacyListAsync(userReadModel.UserId);
        var contactReadModel = await _queryProcessor.ProcessAsync(new GetContactQuery(input.UserId, userReadModel.UserId));
        var photos = await _photoAppService.GetPhotosAsync(userReadModel, contactReadModel);
        var user = _layeredUserService.GetConverter(input.Layer)
            .ToUser(input.UserId, userReadModel, photos, contactReadModel, privacyList);

        var r = new TResolvedPeer
        {
            Chats = new(),
            Peer = new TPeerUser { UserId = userReadModel.UserId },
            Users = new TVector<IUser>(user)
        };

        return r;
    }
}
