// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAutoSaveSettings : IObject
{
    BitArray Flags { get; set; }
    bool Photos { get; set; }
    bool Videos { get; set; }
    long? VideoMaxSize { get; set; }
}
