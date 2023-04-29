// ReSharper disable All

namespace MyTelegram.Schema;

public interface IWebPageAttribute : IObject
{
    BitArray Flags { get; set; }
    TVector<Schema.IDocument>? Documents { get; set; }
    Schema.IThemeSettings? Settings { get; set; }
}
