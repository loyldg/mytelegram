// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatTheme : IObject
{
    string Emoticon { get; set; }
    MyTelegram.Schema.ITheme Theme { get; set; }
    MyTelegram.Schema.ITheme DarkTheme { get; set; }

}
