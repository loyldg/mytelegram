// ReSharper disable All

namespace MyTelegram.Schema;

public interface IMessageRange : IObject
{
    int MinId { get; set; }
    int MaxId { get; set; }

}
