﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Message entity representing an in-text url: <a href="https://google.com/">https://google.com</a>; for <a href="https://google.com/">text urls</a>, use <a href="https://corefork.telegram.org/constructor/messageEntityTextUrl">messageEntityTextUrl</a>.
/// See <a href="https://corefork.telegram.org/constructor/messageEntityUrl" />
///</summary>
[TlObject(0x6ed02538)]
public sealed class TMessageEntityUrl : IMessageEntity
{
    public uint ConstructorId => 0x6ed02538;
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