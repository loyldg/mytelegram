// ReSharper disable All

namespace MyTelegram.Schema;

public interface ITheme : IObject
{
    BitArray Flags { get; set; }
    bool Creator { get; set; }
    bool Default { get; set; }
    bool ForChat { get; set; }
    long Id { get; set; }
    long AccessHash { get; set; }
    string Slug { get; set; }
    string Title { get; set; }
    Schema.IDocument? Document { get; set; }
    TVector<Schema.IThemeSettings>? Settings { get; set; }
    string? Emoticon { get; set; }
    int? InstallsCount { get; set; }
}
