﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.giveawayInfoResults" />
///</summary>
[TlObject(0xcd5570)]
public sealed class TGiveawayInfoResults : IGiveawayInfo
{
    public uint ConstructorId => 0xcd5570;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool Winner { get; set; }
    public bool Refunded { get; set; }
    public int StartDate { get; set; }
    public string? GiftCodeSlug { get; set; }
    public int FinishDate { get; set; }
    public int WinnersCount { get; set; }
    public int ActivatedCount { get; set; }

    public void ComputeFlag()
    {
        if (Winner) { Flags[0] = true; }
        if (Refunded) { Flags[1] = true; }
        if (GiftCodeSlug != null) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(StartDate);
        if (Flags[0]) { writer.Write(GiftCodeSlug); }
        writer.Write(FinishDate);
        writer.Write(WinnersCount);
        writer.Write(ActivatedCount);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Winner = true; }
        if (Flags[1]) { Refunded = true; }
        StartDate = reader.ReadInt32();
        if (Flags[0]) { GiftCodeSlug = reader.ReadString(); }
        FinishDate = reader.ReadInt32();
        WinnersCount = reader.ReadInt32();
        ActivatedCount = reader.ReadInt32();
    }
}