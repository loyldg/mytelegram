// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureSecretSettings : IObject
{
    MyTelegram.Schema.ISecurePasswordKdfAlgo SecureAlgo { get; set; }
    byte[] SecureSecret { get; set; }
    long SecureSecretId { get; set; }

}
