// ReSharper disable All

namespace MyTelegram.Schema;

public interface IJSONObjectValue : IObject
{
    string Key { get; set; }
    MyTelegram.Schema.IJSONValue Value { get; set; }

}
