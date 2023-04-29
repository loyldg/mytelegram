using IPhoto = MyTelegram.Schema.Photos.IPhoto;
using IUserFull = MyTelegram.Schema.Users.IUserFull;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlUserConverter
{
    IUser ToUser(IUserReadModel user,
        long selfUserId);

    IUser ToUser(UserCreatedEvent aggregateEvent);

    IUser ToUser(UserNameUpdatedEvent aggregateEvent);

    Task<IUserFull> ToUserFullAsync(IUserReadModel user,
        long selfUserId,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel
    );

    IList<IUser> ToUserList(IReadOnlyCollection<IUserReadModel> userList,
        long selfUserId);

    IPhoto ToUserPhoto(UserProfilePhotoChangedEvent aggregateEvent);
}
