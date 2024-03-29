﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Defines a user for further interaction.
/// See <a href="https://corefork.telegram.org/constructor/inputPeerUser" />
///</summary>
[TlObject(0xdde8a54c)]
public sealed class TInputPeerUser : IInputPeer
{
    public uint ConstructorId => 0xdde8a54c;
    ///<summary>
    /// User identifier
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// <strong>access_hash</strong> value from the <a href="https://corefork.telegram.org/constructor/user">user</a> constructor
    ///</summary>
    public long AccessHash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(AccessHash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
        AccessHash = reader.ReadInt64();
    }
}