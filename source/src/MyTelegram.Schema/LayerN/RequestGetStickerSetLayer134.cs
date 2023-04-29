// ReSharper disable All

#nullable disable
namespace MyTelegram.Schema.LayerN;

[TlObject(0x2619a90e)]
public sealed class RequestGetStickerSetLayer134 : IRequest<Messages.IStickerSet>
{
    /// <summary>
    ///     See <a href="https://core.telegram.org/type/InputStickerSet" />
    /// </summary>
    public Schema.IInputStickerSet Stickerset { get; set; }

    public uint ConstructorId => 0x2619a90e;

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Stickerset.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Stickerset = br.Deserialize<Schema.IInputStickerSet>();
    }

    public void ComputeFlag()
    {
    }
}
