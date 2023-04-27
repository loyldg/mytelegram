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
    MyTelegram.Schema.IDocument? Document { get; set; }
    TVector<MyTelegram.Schema.IThemeSettings>? Settings { get; set; }
    string? Emoticon { get; set; }
    int? InstallsCount { get; set; }
}
