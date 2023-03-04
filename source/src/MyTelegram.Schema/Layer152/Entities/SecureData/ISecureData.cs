// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureData : IObject
{
    byte[] Data { get; set; }
    byte[] DataHash { get; set; }
    byte[] Secret { get; set; }
}
