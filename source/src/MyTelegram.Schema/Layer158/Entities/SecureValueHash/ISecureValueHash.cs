// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureValueHash : IObject
{
    Schema.ISecureValueType Type { get; set; }
    byte[] Hash { get; set; }
}
