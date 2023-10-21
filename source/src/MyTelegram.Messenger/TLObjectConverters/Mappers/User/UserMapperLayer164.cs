using TUser=MyTelegram.Schema.TUser;

namespace MyTelegram.Messenger.TLObjectConverters.Mappers.User;

public class UserMapperLayer164 : ILayeredMapper,
    IObjectMapper<IUserReadModel, TUser>,
    IObjectMapper<UserCreatedEvent, TUser>,
    IObjectMapper<SignInSuccessEvent, TUser>,
    IObjectMapper<UserItem, TUser>
{
    public TUser Map(IUserReadModel source, TUser destination)
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
        //if (source.EmojiStatusDocumentId.HasValue)
        //{
        //    if (source.EmojiStatusValidUntil.HasValue)
        //    {
        //        destination.EmojiStatus = new TEmojiStatusUntil
        //        {
        //            DocumentId = source.EmojiStatusDocumentId.Value,
        //            Until = source.EmojiStatusValidUntil.Value
        //        };
        //    }
        //    else
        //    {
        //        destination.EmojiStatus = new TEmojiStatus
        //        {
        //            DocumentId = source.EmojiStatusDocumentId.Value
        //        };
        //    }
        //}

        return destination;
    }

    TUser IObjectMapper<IUserReadModel, TUser>.Map(IUserReadModel source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(SignInSuccessEvent source, TUser destination)
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

    TUser IObjectMapper<SignInSuccessEvent, TUser>.Map(SignInSuccessEvent source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(UserCreatedEvent source, TUser destination)
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

    TUser IObjectMapper<UserCreatedEvent, TUser>.Map(UserCreatedEvent source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(UserItem source, TUser destination)
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

    TUser IObjectMapper<UserItem, TUser>.Map(UserItem source)
    {
        return Map(source, new TUser());
    }
}