﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The user's offline status.
/// See <a href="https://corefork.telegram.org/constructor/userStatusOffline" />
///</summary>
[TlObject(0x8c703f)]
public sealed class TUserStatusOffline : IUserStatus
{
    public uint ConstructorId => 0x8c703f;
    ///<summary>
    /// Time the user was last seen online
    ///</summary>
    public int WasOnline { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(WasOnline);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        WasOnline = reader.ReadInt32();
    }
}