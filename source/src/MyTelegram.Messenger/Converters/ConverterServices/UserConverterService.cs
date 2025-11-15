using MyTelegram.Messenger.Services.Impl;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.Converters.ConverterServices;

public class UserConverterService(
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService,
    IPrivacyHelper privacyHelper,
    IContactHelper contactHelper,
    IAccessHashHelper2 accessHashHelper2,
    //IBlockCacheAppService blockCacheAppService,
    IUserStatusCacheAppService userStatusCacheAppService,
    ILayeredService<IUserConverter> userLayeredService,
    ILayeredService<IUserFullConverter> userFullLayeredService,
    ILayeredService<IEmojiStatusConverter> emojiStatusLayeredService,
    ILayeredService<IPhotoConverter> photoLayeredService) : IUserConverterService, ITransientDependency
{
    public async Task<ILayeredUser> GetUserAsync(IRequestWithAccessHashKeyId request, long userId, bool skipSetContactProperties = true,
        bool skipCheckPrivacy = true, int layer = 0)
    {
        var userReadModel = await userAppService.GetAsync(userId);
        if (userReadModel == null)
        {
            throw new RpcException(RpcErrors.RpcErrors400.UserIdInvalid);
        }

        //IReadOnlyCollection<IContactReadModel>? contactReadModels = null;
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null;
        IContactReadModel? myContactReadModel = null;
        IContactReadModel? targetUserContactReadModel = null;
        if (!skipSetContactProperties)
        {
            var contactReadModels =
                await queryProcessor.ProcessAsync(new GetContactListBySelfIdAndTargetUserIdQuery(request.UserId, userId));
            myContactReadModel =
                contactReadModels?.FirstOrDefault(p => p.SelfUserId == request.UserId && p.TargetUserId == userId);
            targetUserContactReadModel =
                contactReadModels?.FirstOrDefault(p => p.SelfUserId == userId && p.TargetUserId == request.UserId);
        }
        var photoReadModels = (await photoAppService.GetPhotosAsync(userReadModel, myContactReadModel)).ToDictionary(k => k.PhotoId);

        if (!skipCheckPrivacy)
        {
            privacyReadModels = await privacyAppService.GetPrivacyListAsync(userId);
        }

        return ToUserCore(request, userReadModel, photoReadModels, myContactReadModel, targetUserContactReadModel,
            privacyReadModels, layer);
    }

    public async Task<List<ILayeredUser>> GetUserListAsync(IRequestWithAccessHashKeyId request, List<long> userIds,
        bool skipSetContactProperties = true,
        bool skipCheckPrivacy = true, int layer = 0)
    {
        var userReadModels = await userAppService.GetListAsync(userIds);
        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModels);
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null;
        IReadOnlyCollection<IContactReadModel>? contactReadModels = null;
        if (!skipSetContactProperties)
        {
            contactReadModels = await queryProcessor.ProcessAsync(new GetContactListQuery(request.UserId, userIds));
        }

        if (!skipCheckPrivacy)
        {
            privacyReadModels = await privacyAppService.GetPrivacyListAsync(userIds);
        }

        return ToUserList(request, userReadModels, photoReadModels, contactReadModels, privacyReadModels, layer);
    }

    public IUserFull ToUserFull(IRequestWithAccessHashKeyId request,
        IUserReadModel userReadModel,
        IReadOnlyCollection<IPhotoReadModel>? photoReadModels,
        IReadOnlyCollection<IContactReadModel>? contactReadModels,
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels, int layer = 0)
    {
        var userId = userReadModel.UserId;
        var isOfficialUserId = userId == MyTelegramConsts.OfficialUserId;
        var phoneCallAvailable = !isOfficialUserId &&
                                 !userReadModel.Bot &&
                                 userId != request.UserId;
        var userFull = userFullLayeredService.GetConverter(layer).ToUserFull(userReadModel);
        userFull.CanPinMessage = !isOfficialUserId;

        if (userReadModel.IsDeleted == true)
        {
            userFull.Settings = new TPeerSettings
            {
                NeedContactsException = true
            };
            userFull.NotifySettings = new TPeerNotifySettings();

            return userFull;
        }

        userFull.PhoneCallsAvailable = phoneCallAvailable;
        userFull.VideoCallsAvailable = phoneCallAvailable;
        userFull.PhoneCallsPrivate = isOfficialUserId;
        //userFull.Blocked = isBlocked;
        //userFull.NotifySettings

        var photos = photoReadModels?.ToDictionary(k => k.PhotoId) ?? [];

        if (userReadModel.ProfilePhotoId != null)
        {
            if (photos.TryGetValue(userReadModel.ProfilePhotoId.Value, out var profilePhotoReadModel))
            {
                userFull.ProfilePhoto = photoLayeredService.GetConverter(layer).ToPhoto(profilePhotoReadModel);
            }
        }

        if (userReadModel.FallbackPhotoId != null)
        {
            if (photos.TryGetValue(userReadModel.FallbackPhotoId.Value, out var fallbackPhotoReadModel))
            {
                userFull.FallbackPhoto = photoLayeredService.GetConverter(layer).ToPhoto(fallbackPhotoReadModel);
                var profilePhotoId = userReadModel.ProfilePhotoId;

                userFull.ProfilePhoto = profilePhotoId is null or 0 ? null : new TPhotoEmpty { Id = profilePhotoId.Value };
            }
        }

        if (request.UserId != userId)
        {
            var myContactReadModel =
                contactReadModels?.FirstOrDefault(p => p.SelfUserId == request.UserId && p.TargetUserId == userId);
            var targetUserContactReadModel =
                contactReadModels?.FirstOrDefault(p => p.SelfUserId == userId && p.TargetUserId == request.UserId);
            var contactType = contactHelper.GetContactType(myContactReadModel, targetUserContactReadModel);

            ApplyPrivacyToUserFull(request.UserId, userFull, privacyReadModels, contactType);

            if (myContactReadModel is { PhotoId: not null })
            {
                if (photos.TryGetValue(myContactReadModel.PhotoId.Value, out var photoReadModel))
                {
                    userFull.PersonalPhoto = photoLayeredService.GetConverter(layer).ToPhoto(photoReadModel);
                    userFull.ProfilePhoto ??= userFull.PersonalPhoto;
                }
            }
        }

        return userFull;
    }

    public async Task<IUserFull> GetUserFullAsync(IRequestWithAccessHashKeyId request, long userId, int layer = 0)
    {
        var userReadModel = await userAppService.GetAsync(userId);
        //var isBlocked = await blockCacheAppService.IsBlockedAsync(selfUserId, userId);
        var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userId);
        IReadOnlyCollection<IContactReadModel>? contactReadModels = null;
        IContactReadModel? myContactReadModel = null;
        if (request.UserId != userId)
        {
            contactReadModels =
              await queryProcessor.ProcessAsync(new GetContactListBySelfIdAndTargetUserIdQuery(request.UserId, userId));
            myContactReadModel =
                contactReadModels?.FirstOrDefault(p => p.SelfUserId == request.UserId && p.TargetUserId == userId);
        }
        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModel, myContactReadModel);

        return ToUserFull(request, userReadModel, photoReadModels, contactReadModels, privacyReadModels, layer);
    }

    public ILayeredUser ToUser(IRequestWithAccessHashKeyId request, IUserReadModel userReadModel, IReadOnlyCollection<IPhotoReadModel>? photoReadModels = null,
        IContactReadModel? contactReadModel = null, IContactReadModel? targetUserContactReadModel = null, IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null, int layer = 0)
    {
        var photos = photoReadModels?.ToDictionary(k => k.PhotoId);

        return ToUserCore(request, userReadModel, photos, contactReadModel, targetUserContactReadModel,
            privacyReadModels, layer);
    }

    public List<ILayeredUser> ToUserList(IRequestWithAccessHashKeyId request, IReadOnlyCollection<IUserReadModel> userReadModels, IReadOnlyCollection<IPhotoReadModel>? photoReadModels = null,
        IReadOnlyCollection<IContactReadModel>? contactReadModels = null, IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null, int layer = 0)
    {
        var users = new List<ILayeredUser>();

        var photos = photoReadModels?
            .DistinctBy(p => p.PhotoId)
            .ToDictionary(k => k.PhotoId) ?? [];

        var targetUserContacts = contactReadModels?
             .DistinctBy(p => p.SelfUserId)
             .ToDictionary(k => k.SelfUserId) ?? [];

        var myContacts = contactReadModels?
             .Where(p => p.SelfUserId == request.UserId)
             .DistinctBy(p => p.TargetUserId)
             .ToDictionary(k => k.TargetUserId) ?? [];

        var groupedPrivacyReadModels = privacyReadModels?.GroupBy(p => p.UserId).ToDictionary(k => k.Key, v => v.ToList()) ?? [];

        foreach (var userReadModel in userReadModels)
        {
            myContacts.TryGetValue(userReadModel.UserId, out var myContactReadModel);
            targetUserContacts.TryGetValue(request.UserId, out var targetUserContactReadModel);
            groupedPrivacyReadModels.TryGetValue(userReadModel.UserId, out var currentUserPrivacyReadModels);
            var user = ToUserCore(request, userReadModel, photos, myContactReadModel,
                targetUserContactReadModel, currentUserPrivacyReadModels, layer);
            users.Add(user);
        }

        return users;
    }

    private ILayeredUser ToUserCore(IRequestWithAccessHashKeyId request, IUserReadModel userReadModel,
        Dictionary<long, IPhotoReadModel>? photoReadModels = null,
        //Dictionary<long, IContactReadModel>? contactReadModels = null,
        IContactReadModel? myContactReadModel = null,
        IContactReadModel? targetUserContactReadModel = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels = null, int layer = 0)
    {
        var user = userLayeredService.GetConverter(layer).ToUser(userReadModel);
        user.AccessHash = 0;
        if (request.AccessHashKeyId != 0)
        {
            user.AccessHash = accessHashHelper2.GenerateAccessHash(request.UserId, request.AccessHashKeyId,
                userReadModel.UserId, AccessHashType.User);
        }

        if (userReadModel.IsDeleted == true)
        {
            user.Deleted = true;
            user.Photo = null;

            return user;
        }

        if (request.UserId == userReadModel.UserId)
        {
            user.Self = true;
        }

        user.Status = userStatusCacheAppService.GetUserStatus(user.Id);
        EmojiStatus? emojiStatus = null;
        if (userReadModel.EmojiStatusDocumentId != null)
        {
            emojiStatus = new EmojiStatus(userReadModel.EmojiStatusDocumentId.Value, userReadModel.EmojiStatusValidUntil);
        }

        user.EmojiStatus = emojiStatusLayeredService.GetConverter(layer).ToEmojiStatus(emojiStatus);
        //var contactReadModel =
        //    contactReadModels?.FirstOrDefault(p =>
        //        p.SelfUserId == selfUserId && p.TargetUserId == userReadModel.UserId);
        var contactType = contactHelper.GetContactType(myContactReadModel, targetUserContactReadModel);
        var photos = photoReadModels ?? [];
        SetUserProfilePhoto(userReadModel, user, photos, layer);
        SetContactPersonalProfilePhoto(user, photos, myContactReadModel, layer);
        ApplyPrivacyToUser(request.UserId, userReadModel, user, photos, contactType, privacyReadModels, layer);

        if (!user.Self)
        {
            switch (contactType)
            {
                case ContactType.None:
                case ContactType.TargetUserIsMyContact:
                    user.Phone = null;
                    break;
                case ContactType.ContactOfTargetUser:
                case ContactType.Mutual:
                    break;
            }
        }

        return user;
    }

    private void ApplyPrivacyToUserFull(long selfUserId,
        IUserFull userFull,
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels,
        ContactType contactType)
    {
        if (selfUserId == userFull.Id)
        {
            return;
        }

        foreach (var privacy in privacyReadModels ?? [])
        {
            switch (privacy.PrivacyType)
            {
                case PrivacyType.PhoneCall:
                    privacyHelper.ApplyPrivacy(privacy, _ =>
                    {
                        userFull.PhoneCallsAvailable = false;
                        userFull.PhoneCallsPrivate = false;
                    }, selfUserId, contactType);
                    break;

                case PrivacyType.ProfilePhoto:
                    privacyHelper.ApplyPrivacy(privacy,
                        _ =>
                        {
                            userFull.ProfilePhoto = null;
                        },
                        selfUserId, contactType);
                    break;

                case PrivacyType.VoiceMessages:
                    privacyHelper.ApplyPrivacy(privacy, _ => { userFull.VoiceMessagesForbidden = true; },
                        selfUserId, contactType);
                    break;
                case PrivacyType.About:
                    privacyHelper.ApplyPrivacy(privacy, _ => { userFull.About = null; }, selfUserId, contactType);
                    break;

                case PrivacyType.Birthday:
                    privacyHelper.ApplyPrivacy(privacy, _ =>
                    {
                        userFull.Birthday = null;
                    }, selfUserId, contactType);
                    break;
            }
        }
    }


    private void ApplyPrivacyToUser(long selfUserId, IUserReadModel userReadModel, ILayeredUser user,
        Dictionary<long, IPhotoReadModel> photos, ContactType contactType,
        IReadOnlyCollection<IPrivacyReadModel>? privacyReadModels, int layer)
    {
        //var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userReadModel.UserId);
        if (selfUserId != userReadModel.UserId && privacyReadModels?.Count > 0)
        {
            photos.TryGetValue(userReadModel.FallbackPhotoId ?? 0, out var fallbackPhotoReadModel);
            //var contactType = contactHelper.GetContactType(selfUserId, userReadModel.UserId, contactReadModels);

            foreach (var privacy in privacyReadModels)
            {
                switch (privacy.PrivacyType)
                {
                    case PrivacyType.StatusTimestamp:
                        privacyHelper.ApplyPrivacy(privacy,
                            _ =>
                            {
                                switch (user.Status)
                                {
                                    case TUserStatusOnline:
                                        break;
                                    case TUserStatusRecently:
                                        break;
                                    case TUserStatusOffline:
                                        user.Status = new TUserStatusRecently();
                                        break;
                                    default:
                                        user.Status = new TUserStatusRecently();
                                        break;
                                }
                            },
                            selfUserId,
                            contactType);
                        break;
                    case PrivacyType.ProfilePhoto:
                        privacyHelper.ApplyPrivacy(privacy,
                            _ => user.Photo = photoLayeredService.GetConverter(layer)
                                .ToProfilePhoto(fallbackPhotoReadModel), selfUserId,
                            contactType);
                        break;
                    case PrivacyType.PhoneNumber:
                        privacyHelper.ApplyPrivacy(privacy, _ => user.Phone = null, selfUserId, contactType);
                        break;
                }
            }
        }
    }

    private void SetContactPersonalProfilePhoto(ILayeredUser user, Dictionary<long, IPhotoReadModel> photos,
        IContactReadModel? contactReadModel,
        int layer)
    {
        if (contactReadModel != null)
        {
            user.Contact = true;
            user.FirstName = contactReadModel.FirstName;
            user.LastName = contactReadModel.LastName;

            if (contactReadModel.PhotoId != null)
            {
                if (photos.TryGetValue(contactReadModel.PhotoId.Value, out var photoReadModel))
                {
                    user.Photo = photoLayeredService.GetConverter(layer).ToProfilePhoto(photoReadModel);
                    if (user.Photo is TUserProfilePhoto profilePhoto)
                    {
                        profilePhoto.Personal = true;
                    }
                }
            }
        }
    }

    private void SetUserProfilePhoto(IUserReadModel userReadModel,
        ILayeredUser user, Dictionary<long, IPhotoReadModel> photoReadModels, int layer)
    {
        if (userReadModel.ProfilePhotoId != null)
        {
            if (photoReadModels.TryGetValue(userReadModel.ProfilePhotoId.Value, out var photoReadModel))
            {
                user.Photo = photoLayeredService.GetConverter(layer).ToProfilePhoto(photoReadModel);
            }
        }
    }
}