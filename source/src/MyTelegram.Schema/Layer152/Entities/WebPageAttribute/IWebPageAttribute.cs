// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWebPageAttribute : IObject
{
    BitArray Flags { get; set; }
    TVector<MyTelegram.Schema.IDocument>? Documents { get; set; }
    MyTelegram.Schema.IThemeSettings? Settings { get; set; }
}
