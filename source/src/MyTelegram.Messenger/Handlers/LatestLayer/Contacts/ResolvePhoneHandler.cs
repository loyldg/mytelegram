namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Resolve a phone number to get user info, if their privacy settings allow it.
/// Possible errors
/// Code Type Description
/// 400 PHONE_NOT_OCCUPIED No user is associated to the specified phone number.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.resolvePhone"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResolvePhoneHandler(IQueryProcessor queryProcessor, IPrivacyAppService privacyAppService, IUserConverterService userConverterService, IPhotoAppService photoAppService) : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResolvePhone, MyTelegram.Schema.Contacts.IResolvedPeer>
{
    protected override async Task<MyTelegram.Schema.Contacts.IResolvedPeer> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestResolvePhone obj)
    {
        var phone = obj.Phone;
        if (phone.StartsWith("+"))
        {
            phone = phone[1..];
        }

        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByPhoneNumberQuery(phone));
        if (userReadModel == null)
        {
            RpcErrors.RpcErrors400.PhoneNotOccupied.ThrowRpcError();
        }

        await privacyAppService.ApplyPrivacyAsync(input.UserId, userReadModel!.UserId, _ =>
        {
            RpcErrors.RpcErrors400.PhoneNotOccupied.ThrowRpcError();
        }, [PrivacyType.AddedByPhone]);
        var privacyList = await privacyAppService.GetPrivacyListAsync(userReadModel.UserId);
        var contactReadModel = await queryProcessor.ProcessAsync(new GetContactQuery(input.UserId, userReadModel.UserId));
        var photos = await photoAppService.GetPhotosAsync(userReadModel, contactReadModel);
        var user = userConverterService.ToUser(input, userReadModel, photos, contactReadModel, null, privacyList, input.Layer);
        var r = new TResolvedPeer
        {
            Chats = [],
            Peer = new TPeerUser
            {
                UserId = userReadModel.UserId
            },
            Users = new TVector<IUser>(user)
        };
        return r;
    }
}