﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
///See <a href="https://core.telegram.org/method/messages.toggleStickerSets" />
///</summary>
[TlObject(0xb5052fea)]
public sealed class RequestToggleStickerSets : IRequest<IBool>
{
    public uint ConstructorId => 0xb5052fea;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool Uninstall { get; set; }
    public bool Archive { get; set; }
    public bool Unarchive { get; set; }
    public TVector<MyTelegram.Schema.IInputStickerSet> Stickersets { get; set; }

    public void ComputeFlag()
    {
        if (Uninstall) { Flags[0] = true; }
        if (Archive) { Flags[1] = true; }
        if (Unarchive) { Flags[2] = true; }

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Serialize(Flags);
        Stickersets.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Flags = br.Deserialize<BitArray>();
        if (Flags[0]) { Uninstall = true; }
        if (Flags[1]) { Archive = true; }
        if (Flags[2]) { Unarchive = true; }
        Stickersets = br.Deserialize<TVector<MyTelegram.Schema.IInputStickerSet>>();
    }
}