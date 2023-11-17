namespace MyTelegram.Messenger.TLObjectConverters.Mappers.User;

public class UserMapper : ILayeredMapper,
    IObjectMapper<IUserReadModel, Schema.TUser>,
    IObjectMapper<UserCreatedEvent, Schema.TUser>,
    IObjectMapper<SignInSuccessEvent, Schema.TUser>,
    IObjectMapper<UserItem, Schema.TUser>
{
    public Schema.TUser Map(IUserReadModel source)
    {
        return Map(source, new Schema.TUser());
    }
    
    public Schema.TUser Map(IUserReadModel source,
        Schema.TUser destination)
    {
        destination.Id = source.UserId;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.AccessHash = source.AccessHash;
        destination.Bot = source.Bot;
        destination.BotInfoVersion = source.BotInfoVersion;
        destination.Username = source.UserName;
        destination.Phone = source.PhoneNumber;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Verified = source.Verified;
        destination.Support = source.Support;
        destination.Premium = source.Premium;
        if (source.EmojiStatusDocumentId.HasValue)
        {
            if (source.EmojiStatusValidUntil.HasValue)
            {
                destination.EmojiStatus = new TEmojiStatusUntil
                {
                    DocumentId = source.EmojiStatusDocumentId.Value,
                    Until = source.EmojiStatusValidUntil.Value
                };
            }
            else
            {
                destination.EmojiStatus = new TEmojiStatus
                {
                    DocumentId = source.EmojiStatusDocumentId.Value
                };
            }
        }

        destination.Color = source.Color;
        destination.BackgroundEmojiId=source.BackgroundEmojiId;

        return destination;
    }


    public Schema.TUser Map(SignInSuccessEvent source)
    {
        return Map(source, new Schema.TUser());
    }


    public Schema.TUser Map(SignInSuccessEvent source,
        Schema.TUser destination)
    {
        destination.Id = source.UserId;
        destination.AccessHash = source.AccessHash;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.Phone = source.PhoneNumber;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Self = true;
        destination.Status = new TUserStatusOnline { Expires = DateTime.UtcNow.AddMinutes(5).ToTimestamp() };

        return destination;
    }
    
    public Schema.TUser Map(UserCreatedEvent source)
    {
        return Map(source, new Schema.TUser());
    }


    public Schema.TUser Map(UserCreatedEvent source,
        Schema.TUser destination)
    {
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Phone = source.PhoneNumber;
        destination.Id = source.UserId;
        destination.AccessHash = source.AccessHash;
        destination.Bot = source.Bot;
        destination.BotInfoVersion = source.BotInfoVersion;
        destination.Self = true;

        return destination;
    }


    public Schema.TUser Map(UserItem source)
    {
        return Map(source, new Schema.TUser());
    }


    public Schema.TUser Map(UserItem source,
        Schema.TUser destination)
    {
        destination.Id = source.UserId;
        destination.Photo = new TUserProfilePhotoEmpty();
        destination.Phone = source.Phone;
        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Username = source.UserName;
        destination.Self = true;

        return destination;
    }
}