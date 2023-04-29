// ReSharper disable All

namespace MyTelegram.Schema;

public interface IReactionCount : IObject
{
    BitArray Flags { get; set; }
    int? ChosenOrder { get; set; }
    Schema.IReaction Reaction { get; set; }
    int Count { get; set; }
}
