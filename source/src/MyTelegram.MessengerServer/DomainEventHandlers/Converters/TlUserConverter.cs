using IPhoto = MyTelegram.Schema.Photos.IPhoto;
using IUserFull = MyTelegram.Schema.Users.IUserFull;
using TPhoto = MyTelegram.Schema.Photos.TPhoto;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlUserConverter : ITlUserConverter
{
    private readonly IObjectMapper _objectMapper;
    private readonly ITlPhotoConverter _photoConverter;
    private readonly IUserStatusCacheAppService _userStatusCacheAppService;

    public TlUserConverter(IObjectMapper objectMapper,
        IUserStatusCacheAppService userStatusCacheAppService,
        ITlPhotoConverter photoConverter)
    {
        _objectMapper = objectMapper;
        _userStatusCacheAppService = userStatusCacheAppService;
        _photoConverter = photoConverter;
    }

    public IUser ToUser(UserCreatedEvent aggregateEvent)
    {
        var tUser = _objectMapper.Map<UserCreatedEvent, TUser>(aggregateEvent);
        tUser.Self = true;
        tUser.Photo = new TUserProfilePhotoEmpty();

        return tUser;
    }

    public IUser ToUser(UserNameUpdatedEvent aggregateEvent)
    {
        return ToUser(aggregateEvent.UserItem);
        //var tUser = _objectMapper.Map<UserItem, TUser>(aggregateEvent.UserItem);
        //tUser.Id = aggregateEvent.UserItem.UserId;
        //tUser.Self = true;
        //tUser.Photo = new TUserProfilePhotoEmpty();
        //tUser.Status = _userStatusAppService.GetUserStatus(aggregateEvent.UserItem.UserId);

        //return tUser;
    }

    public IUser ToUser(IUserReadModel user,
        long selfUserId)
    {
        var tUser = _objectMapper.Map<IUserReadModel, TUser>(user);
        tUser.Self = selfUserId == user.UserId;
        tUser.Photo = _photoConverter.GetProfilePhoto(user.ProfilePhoto);

        tUser.Status =
            _userStatusCacheAppService
                .GetUserStatus(user
                    .UserId); // GetUserStatus(cachedStatus?.LastUpdateDate ?? user.LastUpdateDate, cachedStatus?.Online ?? user.IsOnline);
        if (user.Bot)
        {
            tUser.BotInfoVersion = 1;
            tUser.BotChatHistory = true; // bot.AllowAccessGroupMessages;
        }

        return tUser;
    }

    public Task<IUserFull> ToUserFullAsync(IUserReadModel user,
        long selfUserId,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel)
    {
        var isOfficialId = user.UserId == MyTelegramServerDomainConsts.OfficialUserId;
        var tUser = ToUser(user, selfUserId);
        var fullUser = new TUserFull
        {
            About = user.About,
            Blocked = false,
            Id = user.UserId,
            CanPinMessage = !isOfficialId,
            PhoneCallsAvailable = !user.Bot && !isOfficialId,
            VideoCallsAvailable = !user.Bot && !isOfficialId,
            PhoneCallsPrivate = isOfficialId,
            PinnedMsgId = user.PinnedMsgId,
            ProfilePhoto = user.ProfilePhoto.ToTObject<Schema.IPhoto>() ?? new TPhotoEmpty(),
            Settings = new TPeerSettings(),
            NotifySettings =
                _objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(
                    peerNotifySettingsReadModel?.NotifySettings ?? PeerNotifySettings.DefaultSettings)
        };
        var userFull = new Schema.Users.TUserFull
        {
            Chats = new TVector<IChat>(),
            FullUser = fullUser,
            Users = new TVector<IUser>(tUser)
        };

        return Task.FromResult<IUserFull>(userFull);
    }

    public IList<IUser> ToUserList(IReadOnlyCollection<IUserReadModel> userList,
        long selfUserId)
    {
        var users = new List<IUser>();
        foreach (var user in userList) users.Add(ToUser(user, selfUserId));

        return users;
    }

    public IPhoto ToUserPhoto(UserProfilePhotoChangedEvent aggregateEvent)
    {
        var user = ToUser(aggregateEvent.UserItem);
        var photo = aggregateEvent.UserItem.ProfilePhoto.ToTObject<Schema.IPhoto>();
        return new TPhoto { Photo = photo, Users = new TVector<IUser>(user) };
    }

    private IUser ToUser(UserItem userItem)
    {
        var tUser = _objectMapper.Map<UserItem, TUser>(userItem);
        tUser.Id = userItem.UserId;
        tUser.Self = true;
        tUser.Status = _userStatusCacheAppService.GetUserStatus(userItem.UserId);
        tUser.Photo = _photoConverter.GetProfilePhoto(userItem.ProfilePhoto);

        return tUser;
    }

    //private IUser ToUser(UserItem userItem)
    //{
    //    var tUser = _objectMapper.Map<UserItem, TUser>(userItem);
    //    tUser.Id = userItem.UserId;
    //    tUser.Self = true;
    //    tUser.Status = _userStatusCacheAppService.GetUserStatus(userItem.UserId);
    //    tUser.Photo = _photoConverter.GetProfilePhoto(userItem.ProfilePhoto);

    //    return tUser;
    //}

    //private IUser ToUser(UserCreatedEvent aggregateEvent)
    //{
    //    var tUser = _objectMapper.Map<UserCreatedEvent, TUser>(source: aggregateEvent);
    //    tUser.Self = true;
    //    tUser.Photo = new TUserProfilePhotoEmpty();

    //    return tUser;
    //}
}