namespace MyTelegram.Schema;

public partial interface ILayeredDocument : IDocument
{
    long AccessHash { get; set; }
    TVector<MyTelegram.Schema.IDocumentAttribute> Attributes { get; set; }
}