// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageEntity : IObject
{
    int Offset { get; set; }
    int Length { get; set; }
}
