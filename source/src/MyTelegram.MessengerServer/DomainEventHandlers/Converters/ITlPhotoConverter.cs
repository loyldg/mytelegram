namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlPhotoConverter
{
    IChatPhoto GetChatPhoto(byte[]? photo);
    IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto);
}