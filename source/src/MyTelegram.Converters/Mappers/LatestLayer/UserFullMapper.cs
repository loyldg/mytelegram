using System.Diagnostics.CodeAnalysis;

namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class UserFullMapper
    : IObjectMapper<IUserFullReadModel, TUserFull>,
        IObjectMapper<IUserReadModel, TUserFull>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;
    

    public TUserFull Map(IUserFullReadModel source)
    {
        return Map(source, new TUserFull());
    }

    public TUserFull Map(
        IUserFullReadModel source,
        TUserFull destination
    )
    {
        destination.BusinessWorkHours = source.BusinessWorkHours;
        destination.BusinessLocation = source.BusinessLocation;
        destination.BusinessGreetingMessage = source.BusinessGreetingMessage;
        destination.BusinessAwayMessage = source.BusinessAwayMessage;
        destination.BusinessIntro = source.BusinessIntro;

        destination.Id = source.UserId;
        destination.Settings = new TPeerSettings();

        return destination;
    }

    [return: NotNullIfNotNull("source")]
    public TUserFull? Map(IUserReadModel source)
    {
        return Map(source, new TUserFull());
    }

    [return: NotNullIfNotNull("source")]
    public TUserFull? Map(IUserReadModel source, TUserFull destination)
    {
        destination.Id = source.UserId;
        destination.About = source.About;
        destination.Settings = new TPeerSettings();
        destination.ReadDatesPrivate = source.GlobalPrivacySettings?.HideReadMarks ?? false;
        if (source.Birthday != null)
        {
            destination.Birthday = new TBirthday
            {
                Day = source.Birthday.Day,
                Month = source.Birthday.Month,
                Year = source.Birthday.Year
            };
        }

        return destination;
    }
}