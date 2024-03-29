﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Message entity representing underlined text.
/// See <a href="https://corefork.telegram.org/constructor/messageEntityUnderline" />
///</summary>
[TlObject(0x9c4e7e8b)]
public sealed class TMessageEntityUnderline : IMessageEntity
{
    public uint ConstructorId => 0x9c4e7e8b;
    ///<summary>
    /// Offset of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    public int Offset { get; set; }

    ///<summary>
    /// Length of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    public int Length { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Offset);
        writer.Write(Length);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Offset = reader.ReadInt32();
        Length = reader.ReadInt32();
    }
}