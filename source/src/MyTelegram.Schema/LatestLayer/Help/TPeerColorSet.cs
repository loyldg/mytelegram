﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/colors">color palette that can be used in message accents »</a>.
/// See <a href="https://corefork.telegram.org/constructor/help.peerColorSet" />
///</summary>
[TlObject(0x26219a58)]
public sealed class TPeerColorSet : IPeerColorSet
{
    public uint ConstructorId => 0x26219a58;
    ///<summary>
    /// A list of 1-3 colors in RGB format, describing the accent color.
    ///</summary>
    public TVector<int> Colors { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Colors);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Colors = reader.Read<TVector<int>>();
    }
}