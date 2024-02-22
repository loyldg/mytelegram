using IPhoto = MyTelegram.Schema.Photos.IPhoto;
using IUserFull = MyTelegram.Schema.Users.IUserFull;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;
using TPhoto = MyTelegram.Schema.Photos.TPhoto;
using TUserFull = MyTelegram.Schema.Users.TUserFull;

namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class UserConverterLatest : UserConverterBase, IUserConverterLatest
{
    private readonly IBlockCacheAppService _blockCacheAppService;
    private readonly ILayeredService<IPeerNotifySettingsConverter> _layeredPeerNotifySettingsService;
    private readonly ILayeredService<IPeerSettingsConverter> _layeredPeerSettingsConverter;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService;
    private readonly IPrivacyHelper _privacyHelper;
    private readonly IUserStatusCacheAppService _userStatusCacheAppService;
    private IPhotoConverter? _photoConverter;

    public UserConverterLatest(IObjectMapper objectMapper,
        IUserStatusCacheAppService userStatusCacheAppService,
        //ITlPhotoConverterLayer143 photoConverter,
        IPrivacyHelper privacyHelper,
        IBlockCacheAppService blockCacheAppService,
        ILayeredService<IPhotoConverter> layeredPhotoService,
        ILayeredService<IPeerSettingsConverter> layeredPeerSettingsConverter,
        ILayeredService<IPeerNotifySettingsConverter> layeredPeerNotifySettingsService)
    {
        ObjectMapper = objectMapper;
        _userStatusCacheAppService = userStatusCacheAppService;
        _privacyHelper = privacyHelper;
        _blockCacheAppService = blockCacheAppService;
        _layeredPhotoService = layeredPhotoService;
        _layeredPeerSettingsConverter = layeredPeerSettingsConverter;
        _layeredPeerNotifySettingsService = layeredPeerNotifySettingsService;
        //_layeredPhotoService = layeredPhotoService;
    }

    public override int Layer => Layers.LayerLatest;
    protected IObjectMapper ObjectMapper { get; }
    public virtual IUser ToUser(SignInSuccessEvent aggregateEvent)
    {
        return ObjectMapper.Map<SignInSuccessEvent, TUser>(aggregateEvent);
    }

    public virtual IUser ToUser(UserCreatedEvent aggregateEvent)
    {
        return ObjectMapper.Map<UserCreatedEvent, TUser>(aggregateEvent);
    }

    public virtual IUser ToUser(UserNameUpdatedEvent aggregateEvent)
    {
        return ToUser(aggregateEvent.UserItem);
    }

    public ILayeredUser ToUser(
        long selfUserId /*,BotReadModel bot*/,
        IUserReadModel user,
        IReadOnlyCollection<IPhotoReadModel>? photos,
        IContactReadModel? contactReadModel = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null)
    {
        var tUser = ToUser(user);
        tUser.Self = selfUserId == user.UserId;
        SetUserStatusAndPhoto(tUser, photos?.FirstOrDefault(p => p.PhotoId == user.ProfilePhotoId));

        if (user.Bot)
        {
            tUser.BotInfoVersion = 1;
            tUser.BotChatHistory = true; // bot.AllowAccessGroupMessages;
        }

        if (!tUser.Self && privacies?.Count > 0)
        {
            var fallbackPhotoReadModel = photos?.FirstOrDefault(p => p.PhotoId == user.FallbackPhotoId);
            ApplyPrivacyToUser(selfUserId, fallbackPhotoReadModel, tUser, privacies);
        }

        SetContactPersonalProfilePhoto(contactReadModel, tUser, photos);

        return tUser;
    }

    public async Task<IUserFull> ToUserFullAsync(
        long selfUserId,
        IUserReadModel user,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel = null,
        IPeerSettingsReadModel? peerSettingsReadModel = null,
        IReadOnlyCollection<IPhotoReadModel>? photos = null,
        IBotReadModel? bot = null,
        IContactReadModel? contactReadModel = null,
        ContactType? contactType = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        //var isOfficialId = user.UserId == MyTelegramServerDomainConsts.OfficialUserId;
        var tUser = ToUser(selfUserId, user, photos, contactReadModel, privacies);
        var isBlocked = await _blockCacheAppService.IsBlockedAsync(selfUserId, user.UserId);

        //var notifySettings = ObjectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
        //    peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings);
        //notifySettings.AndroidSound = new TNotificationSoundDefault();
        //notifySettings.IosSound = new TNotificationSoundDefault();
        //notifySettings.OtherSound = new TNotificationSoundLocal
        //{
        //    Data = "default",
        //    Title = "default"
        var notifySettings = GetPeerNotifySettings(peerNotifySettingsReadModel?.NotifySettings);

        var fullUser = await GetUserFullCoreAsync(selfUserId, user, photos);
        fullUser.NotifySettings = notifySettings;
        fullUser.Blocked = isBlocked;
        fullUser.Settings = _layeredPeerSettingsConverter.GetConverter(GetLayer())
            .ToPeerSettings(peerSettingsReadModel, contactType);


        CallAfterUserFullCreated(user, tUser, fullUser);

        ApplyPrivacyToUserFull(user, fullUser, tUser, privacies, photos, selfUserId);

        SetContactPersonalProfilePhoto(contactReadModel, tUser, photos, fullUser);

        var userFull = new TUserFull
        {
            Chats = new TVector<IChat>(),
            FullUser = fullUser,
            Users = new TVector<IUser>(tUser)
        };

        return userFull;
    }

    public IList<ILayeredUser> ToUserList(
        long selfUserId,
        IReadOnlyCollection<IUserReadModel> userList,
        IReadOnlyCollection<IPhotoReadModel>? photos = null,
        IReadOnlyCollection<IContactReadModel>? contactList = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    )
    {
        var tUserList = new List<ILayeredUser>();
        var contactDict = contactList?.DistinctBy(p => p.TargetUserId).ToDictionary(k => k.TargetUserId, v => v);
        foreach (var user in userList)
        {
            IContactReadModel? contactReadModel = null;
            contactDict?.TryGetValue(user.UserId, out contactReadModel);
            List<IPrivacyReadModel>? privacyListForUser = null;
            tUserList.Add(ToUser(selfUserId, user, photos, contactReadModel, privacyListForUser));
        }

        return tUserList;
    }

    public IPhoto ToUserPhoto(IUserReadModel userReadModel, IPhotoReadModel? photoReadModel)
    {
        var user = ToUser(userReadModel.UserId, userReadModel,
            photoReadModel == null ? null : new List<IPhotoReadModel> { photoReadModel });
        var photo = GetPhotoConverter().ToPhoto(photoReadModel);

        return new TPhoto
        {
            Photo = photo,
            Users = new TVector<IUser>(user)
        };
    }

    protected virtual void ApplyProfilePhotoPrivacyToUserFull(
        //long selfUserId,
        //IPrivacyReadModel privacy,
        IUserReadModel userReadModel,
        Schema.IUserFull userFull,
        ILayeredUser user,
        IReadOnlyCollection<IPhotoReadModel>? photos
    )
    {
        user.Photo = null;
        userFull.ProfilePhoto = null;
        //_privacyHelper.ApplyPrivacy(privacy, () =>
        switch (userFull.FallbackPhoto)
        {
            case Schema.TPhoto photo:
                user.Photo = new TUserProfilePhoto
                {
                    DcId = photo.DcId,
                    HasVideo = photo.VideoSizes?.Count > 0,
                    Personal = false,
                    PhotoId = photo.Id
                };
                break;
        }
    }

    protected virtual void ApplyVoiceMessagePrivacyToUserFull(Schema.IUserFull userFull)
    {
    }

    protected virtual void CallAfterUserFullCreated(IUserReadModel user, ILayeredUser layeredUser,
        Schema.IUserFull userFull)
    {
    }

    protected virtual IPeerNotifySettings GetPeerNotifySettings(PeerNotifySettings? peerNotifySettings)
    {
        return _layeredPeerNotifySettingsService.GetConverter(GetLayer()).ToPeerNotifySettings(peerNotifySettings);
    }

    protected virtual IPhotoConverter GetPhotoConverter()
    {
        return _photoConverter ??= _layeredPhotoService.GetConverter(GetLayer());
    }

    protected virtual Task<Schema.IUserFull> GetUserFullCoreAsync(
        long selfUserId,
        IUserReadModel user,
        IReadOnlyCollection<IPhotoReadModel>? photos)
    {
        var isOfficialId = user.UserId == MyTelegramServerDomainConsts.OfficialUserId;
        //var isBlocked = await _blockCacheAppService.IsBlockedAsync(selfUserId, user.UserId);
        var fullUser = new Schema.TUserFull
        {
            Id = user.UserId,
            About = user.About,
            //Blocked = isBlocked,
            CanPinMessage = !isOfficialId,
            PhoneCallsAvailable = !user.Bot && !isOfficialId,
            VideoCallsAvailable = !user.Bot && !isOfficialId,
            PhoneCallsPrivate = isOfficialId,
            PinnedMsgId = user.PinnedMsgId,
            //ProfilePhoto = user.ProfilePhoto.ToTObject<Schema.IPhoto>() ?? new TPhotoEmpty(),
            Settings = new TPeerSettings(),
            ProfilePhoto = GetPhotoConverter().ToPhoto(photos?.FirstOrDefault(p => p.PhotoId == user.ProfilePhotoId)),
            FallbackPhoto = GetPhotoConverter().ToPhoto(photos?.FirstOrDefault(p => p.PhotoId == user.FallbackPhotoId)),
            ReadDatesPrivate = user.GlobalPrivacySettings?.HideReadMarks ?? false,
        };

        return Task.FromResult<Schema.IUserFull>(fullUser);
    }

    protected virtual void SetContactPersonalProfilePhoto(IContactReadModel? contactReadModel, ILayeredUser layeredUser,
                                IReadOnlyCollection<IPhotoReadModel>? photos, Schema.IUserFull? userFull = null)
    {
        if (contactReadModel != null)
        {
            layeredUser.Contact = true;
            layeredUser.FirstName = contactReadModel.FirstName;
            layeredUser.LastName = contactReadModel.LastName;

            if (contactReadModel.PhotoId.HasValue)
            {
                var personalPhoto = photos?.FirstOrDefault(p => p.PhotoId == contactReadModel.PhotoId);

                if (personalPhoto != null)
                {
                    layeredUser.Photo = GetPhotoConverter().ToProfilePhoto(personalPhoto);
                    if (layeredUser.Photo is TUserProfilePhoto photo)
                    {
                        photo.Personal = true;
                    }

                    if (userFull != null)
                    {
                        userFull.PersonalPhoto = GetPhotoConverter().ToPhoto(personalPhoto);
                    }
                }
            }
        }
    }
    protected void SetUserStatusAndPhoto(ILayeredUser user,
        IPhotoReadModel? profilePhoto = null
    //byte[]? profilePhoto
    //IUserProfilePhoto? profilePhoto
    )
    {
        user.Status = _userStatusCacheAppService.GetUserStatus(user.Id);
        user.Photo = GetPhotoConverter().ToProfilePhoto(profilePhoto);
    }

    protected override ILayeredUser ToUser(IUserReadModel user)
    {
        return ObjectMapper.Map<IUserReadModel, TUser>(user);
    }

    protected virtual ILayeredUser ToUser(UserItem userItem)
    {
        return ObjectMapper.Map<UserItem, TUser>(userItem);
    }

    private void ApplyPrivacyToUser(
                    long selfUserId,
        IPhotoReadModel? fallbackPhotoReadModel,
        ILayeredUser user,
        IReadOnlyCollection<IPrivacyReadModel> privacies
    )
    {
    }

    private void ApplyPrivacyToUserFull(
        IUserReadModel userReadModel,
        Schema.IUserFull userFull,
        ILayeredUser user,
        IReadOnlyCollection<IPrivacyReadModel>? privacies,
        IReadOnlyCollection<IPhotoReadModel>? photos,
        long selfUserId
    )
    {
        if (selfUserId == userReadModel.UserId)
        {
        }
    }
}