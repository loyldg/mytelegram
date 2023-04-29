// ReSharper disable All

namespace MyTelegram.Schema;

public interface IEmojiKeywordsDifference : IObject
{
    string LangCode { get; set; }
    int FromVersion { get; set; }
    int Version { get; set; }
    TVector<Schema.IEmojiKeyword> Keywords { get; set; }
}
