// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISecureValueError : IObject
{
    Schema.ISecureValueType Type { get; set; }
    string Text { get; set; }
}
