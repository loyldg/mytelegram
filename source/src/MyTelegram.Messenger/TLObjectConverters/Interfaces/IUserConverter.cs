using IPhoto = MyTelegram.Schema.Photos.IPhoto;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IUserConverter : ILayeredConverter
{
    ILayeredUser ToUser(
        long selfUserId,
        IUserReadModel user,
        IReadOnlyCollection<IPhotoReadModel>? photos,
        IContactReadModel? contactReadModel = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null
        );

    IUser ToUser(SignInSuccessEvent aggregateEvent);

    IUser ToUser(UserCreatedEvent aggregateEvent);

    IUser ToUser(UserNameUpdatedEvent aggregateEvent);

    Task<Schema.Users.IUserFull> ToUserFullAsync(
        long selfUserId,
        IUserReadModel user,
        IPeerNotifySettingsReadModel? peerNotifySettingsReadModel = null,
        IPeerSettingsReadModel? peerSettingsReadModel = null,
        IReadOnlyCollection<IPhotoReadModel>? photos = null,
        IBotReadModel? bot = null,
        IContactReadModel? contactReadModel = null,
        ContactType? contactType = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null
    );

    IList<ILayeredUser> ToUserList(
        long selfUserId,
        IReadOnlyCollection<IUserReadModel> userList,
        IReadOnlyCollection<IPhotoReadModel>? photos = null,
        IReadOnlyCollection<IContactReadModel>? contactList = null,
        IReadOnlyCollection<IPrivacyReadModel>? privacies = null);

    IPhoto ToUserPhoto(IUserReadModel userReadModel, IPhotoReadModel? photoReadModel);
}