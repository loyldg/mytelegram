namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlPhotoConverter
{
    IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto);
    IChatPhoto GetChatPhoto(byte[]? photo);
}