// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputWebDocument : IObject
{
    string Url { get; set; }
    int Size { get; set; }
    string MimeType { get; set; }
    TVector<MyTelegram.Schema.IDocumentAttribute> Attributes { get; set; }
}
