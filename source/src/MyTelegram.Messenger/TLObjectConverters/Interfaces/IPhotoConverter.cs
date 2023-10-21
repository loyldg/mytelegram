namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPhotoConverter : ILayeredConverter
{
    IPhoto ToPhoto(IPhotoReadModel? photoReadModel);
    IUserProfilePhoto ToProfilePhoto(IPhotoReadModel? photoReadModel);
    IChatPhoto ToChatPhoto(IPhotoReadModel? photoReadModel);

}
