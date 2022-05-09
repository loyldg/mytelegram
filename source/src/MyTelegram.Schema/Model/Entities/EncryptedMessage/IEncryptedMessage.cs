// ReSharper disable All

namespace MyTelegram.Schema;

public interface IEncryptedMessage : IObject
{
    long RandomId { get; set; }
    int ChatId { get; set; }
    int Date { get; set; }
    byte[] Bytes { get; set; }

}
