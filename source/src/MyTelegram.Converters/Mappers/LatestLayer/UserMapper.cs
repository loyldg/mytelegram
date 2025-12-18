namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class UserMapper
    : IObjectMapper<IUserReadModel, TUser>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;
    

    public TUser Map(IUserReadModel source)
    {
        return Map(source, new TUser());
    }

    public TUser Map(
        IUserReadModel source,
        TUser destination
    )
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

        destination.Color = source.Color.ToPeerColor();
        destination.ProfileColor = source.ProfileColor.ToPeerColor();
        destination.ContactRequirePremium = source.GlobalPrivacySettings?.NewNoncontactPeersRequirePremium ?? false;
        destination.BotHasMainApp = source.BotHasMainApp;
        destination.BotActiveUsers = source.BotActiveUsers;

        return destination;
    }
}