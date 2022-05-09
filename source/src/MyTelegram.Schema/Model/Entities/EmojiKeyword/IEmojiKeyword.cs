// ReSharper disable All

namespace MyTelegram.Schema;

public interface IEmojiKeyword : IObject
{
    string Keyword { get; set; }
    TVector<string> Emoticons { get; set; }

}
