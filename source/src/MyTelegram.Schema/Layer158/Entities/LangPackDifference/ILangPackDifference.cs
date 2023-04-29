// ReSharper disable All

namespace MyTelegram.Schema;

public interface ILangPackDifference : IObject
{
    string LangCode { get; set; }
    int FromVersion { get; set; }
    int Version { get; set; }
    TVector<Schema.ILangPackString> Strings { get; set; }
}
