// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureSecretSettings : IObject
{
    Schema.ISecurePasswordKdfAlgo SecureAlgo { get; set; }
    byte[] SecureSecret { get; set; }
    long SecureSecretId { get; set; }
}
