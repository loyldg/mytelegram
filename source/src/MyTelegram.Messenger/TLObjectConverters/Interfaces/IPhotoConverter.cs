namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPhotoConverter : ILayeredConverter
{
    //IChatPhoto GetChatPhoto(byte[]? photo);

    //IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto);

    IPhoto ToPhoto(IPhotoReadModel? photoReadModel);
    IUserProfilePhoto ToProfilePhoto(IPhotoReadModel? photoReadModel);
    IChatPhoto ToChatPhoto(IPhotoReadModel? photoReadModel);

}
