// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputFile : IObject
{
    long Id { get; set; }
    int Parts { get; set; }
    string Name { get; set; }
}
