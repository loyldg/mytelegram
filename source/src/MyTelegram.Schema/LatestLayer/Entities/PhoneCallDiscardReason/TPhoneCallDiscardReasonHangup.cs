﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The phone call was ended normally
/// See <a href="https://corefork.telegram.org/constructor/phoneCallDiscardReasonHangup" />
///</summary>
[TlObject(0x57adc690)]
public sealed class TPhoneCallDiscardReasonHangup : IPhoneCallDiscardReason
{
    public uint ConstructorId => 0x57adc690;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}