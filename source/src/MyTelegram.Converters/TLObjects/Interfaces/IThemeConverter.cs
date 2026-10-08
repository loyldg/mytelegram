namespace MyTelegram.Converters.TLObjects.Interfaces;

public interface IThemeConverter : ILayeredConverter
{
    ITheme ToTheme(Theme theTheme, IReadOnlyCollection<IDocumentReadModel> documentReadModels);

    ITheme ToTheme(long selfUserId, IThemeReadModel themeReadModel,
        IReadOnlyCollection<IDocumentReadModel> documentReadModels);
}