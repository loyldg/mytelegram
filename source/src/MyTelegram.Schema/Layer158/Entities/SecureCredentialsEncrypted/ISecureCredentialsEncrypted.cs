// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureCredentialsEncrypted : IObject
{
    byte[] Data { get; set; }
    byte[] Hash { get; set; }
    byte[] Secret { get; set; }
}
