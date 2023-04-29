// ReSharper disable All

namespace MyTelegram.Schema;

public interface IJSONObjectValue : IObject
{
    string Key { get; set; }
    Schema.IJSONValue Value { get; set; }
}
