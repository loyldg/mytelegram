// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureValueHash : IObject
{
    MyTelegram.Schema.ISecureValueType Type { get; set; }
    byte[] Hash { get; set; }

}
