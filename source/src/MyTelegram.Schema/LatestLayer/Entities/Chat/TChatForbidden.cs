﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A group to which the user has no access. E.g., because the user was kicked from the group.
/// See <a href="https://corefork.telegram.org/constructor/chatForbidden" />
///</summary>
[TlObject(0x6592a1a7)]
public sealed class TChatForbidden : IChat
{
    public uint ConstructorId => 0x6592a1a7;
    ///<summary>
    /// User identifier
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Group name
    ///</summary>
    public string Title { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
        writer.Write(Title);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt64();
        Title = reader.ReadString();
    }
}