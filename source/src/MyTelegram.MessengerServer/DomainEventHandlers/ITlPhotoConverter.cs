namespace MyTelegram.MessengerServer.DomainEventHandlers;

public interface ITlPhotoConverter
{
    IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto);
    IChatPhoto GetChatPhoto(byte[]? photo);
}