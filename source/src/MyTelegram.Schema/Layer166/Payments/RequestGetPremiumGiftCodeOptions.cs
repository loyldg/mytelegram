﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getPremiumGiftCodeOptions" />
///</summary>
[TlObject(0x2757ba54)]
public sealed class RequestGetPremiumGiftCodeOptions : IRequest<TVector<MyTelegram.Schema.IPremiumGiftCodeOption>>
{
    public uint ConstructorId => 0x2757ba54;
    public BitArray Flags { get; set; } = new BitArray(32);
    public MyTelegram.Schema.IInputPeer? BoostPeer { get; set; }

    public void ComputeFlag()
    {
        if (BoostPeer != null) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(BoostPeer); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { BoostPeer = reader.Read<MyTelegram.Schema.IInputPeer>(); }
    }
}
