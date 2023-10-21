﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Got popular <a href="https://corefork.telegram.org/api/reactions">message reactions</a>
/// See <a href="https://corefork.telegram.org/method/messages.getTopReactions" />
///</summary>
[TlObject(0xbb8125ba)]
public sealed class RequestGetTopReactions : IRequest<MyTelegram.Schema.Messages.IReactions>
{
    public uint ConstructorId => 0xbb8125ba;
    ///<summary>
    /// Maximum number of results to return, <a href="https://corefork.telegram.org/api/offsets">see pagination</a>
    ///</summary>
    public int Limit { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash for pagination, for more info click here</a>
    ///</summary>
    public long Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Limit);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Limit = reader.ReadInt32();
        Hash = reader.ReadInt64();
    }
}