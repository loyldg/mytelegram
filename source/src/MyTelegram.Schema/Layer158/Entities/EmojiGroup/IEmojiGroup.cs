// ReSharper disable All

namespace MyTelegram.Schema;

public interface IEmojiGroup : IObject
{
    string Title { get; set; }
    long IconEmojiId { get; set; }
    TVector<string> Emoticons { get; set; }
}
