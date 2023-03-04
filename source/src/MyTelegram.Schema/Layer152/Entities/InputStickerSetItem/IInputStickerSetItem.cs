// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputStickerSetItem : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IInputDocument Document { get; set; }
    string Emoji { get; set; }
    MyTelegram.Schema.IMaskCoords? MaskCoords { get; set; }
}
