// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputEncryptedChat : IObject
{
    int ChatId { get; set; }
    long AccessHash { get; set; }
}
