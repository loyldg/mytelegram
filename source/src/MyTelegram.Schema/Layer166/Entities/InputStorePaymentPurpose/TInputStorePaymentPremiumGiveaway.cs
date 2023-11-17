﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/inputStorePaymentPremiumGiveaway" />
///</summary>
[TlObject(0x7c9375e6)]
public sealed class TInputStorePaymentPremiumGiveaway : IInputStorePaymentPurpose
{
    public uint ConstructorId => 0x7c9375e6;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool OnlyNewSubscribers { get; set; }
    public MyTelegram.Schema.IInputPeer BoostPeer { get; set; }
    public TVector<MyTelegram.Schema.IInputPeer>? AdditionalPeers { get; set; }
    public TVector<string>? CountriesIso2 { get; set; }
    public long RandomId { get; set; }
    public int UntilDate { get; set; }
    public string Currency { get; set; }
    public long Amount { get; set; }

    public void ComputeFlag()
    {
        if (OnlyNewSubscribers) { Flags[0] = true; }
        if (AdditionalPeers?.Count > 0) { Flags[1] = true; }
        if (CountriesIso2?.Count > 0) { Flags[2] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(BoostPeer);
        if (Flags[1]) { writer.Write(AdditionalPeers); }
        if (Flags[2]) { writer.Write(CountriesIso2); }
        writer.Write(RandomId);
        writer.Write(UntilDate);
        writer.Write(Currency);
        writer.Write(Amount);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { OnlyNewSubscribers = true; }
        BoostPeer = reader.Read<MyTelegram.Schema.IInputPeer>();
        if (Flags[1]) { AdditionalPeers = reader.Read<TVector<MyTelegram.Schema.IInputPeer>>(); }
        if (Flags[2]) { CountriesIso2 = reader.Read<TVector<string>>(); }
        RandomId = reader.ReadInt64();
        UntilDate = reader.ReadInt32();
        Currency = reader.ReadString();
        Amount = reader.ReadInt64();
    }
}