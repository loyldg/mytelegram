// ReSharper disable All

namespace MyTelegram.Schema;

public interface IAvailableReaction : IObject
{
    BitArray Flags { get; set; }
    bool Inactive { get; set; }
    bool Premium { get; set; }
    string Reaction { get; set; }
    string Title { get; set; }
    MyTelegram.Schema.IDocument StaticIcon { get; set; }
    MyTelegram.Schema.IDocument AppearAnimation { get; set; }
    MyTelegram.Schema.IDocument SelectAnimation { get; set; }
    MyTelegram.Schema.IDocument ActivateAnimation { get; set; }
    MyTelegram.Schema.IDocument EffectAnimation { get; set; }
    MyTelegram.Schema.IDocument? AroundAnimation { get; set; }
    MyTelegram.Schema.IDocument? CenterIcon { get; set; }

}
