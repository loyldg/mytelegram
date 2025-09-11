namespace MyTelegram.Schema;

public interface ILayeredDocument : IDocument
{
    long AccessHash { get; set; }
    TVector<MyTelegram.Schema.IDocumentAttribute> Attributes { get; set; }
}