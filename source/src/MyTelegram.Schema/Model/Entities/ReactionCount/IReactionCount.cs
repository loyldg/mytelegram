// ReSharper disable All

namespace MyTelegram.Schema;

public interface IReactionCount : IObject
{
    BitArray Flags { get; set; }
    bool Chosen { get; set; }
    string Reaction { get; set; }
    int Count { get; set; }

}
