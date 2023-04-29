// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputStickerSetItem : IObject
{
    BitArray Flags { get; set; }
    Schema.IInputDocument Document { get; set; }
    string Emoji { get; set; }
    Schema.IMaskCoords? MaskCoords { get; set; }
    string? Keywords { get; set; }
}
