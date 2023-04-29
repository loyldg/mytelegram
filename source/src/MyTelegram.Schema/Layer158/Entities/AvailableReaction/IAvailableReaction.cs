// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAvailableReaction : IObject
{
    BitArray Flags { get; set; }
    bool Inactive { get; set; }
    bool Premium { get; set; }
    string Reaction { get; set; }
    string Title { get; set; }
    Schema.IDocument StaticIcon { get; set; }
    Schema.IDocument AppearAnimation { get; set; }
    Schema.IDocument SelectAnimation { get; set; }
    Schema.IDocument ActivateAnimation { get; set; }
    Schema.IDocument EffectAnimation { get; set; }
    Schema.IDocument? AroundAnimation { get; set; }
    Schema.IDocument? CenterIcon { get; set; }
}
