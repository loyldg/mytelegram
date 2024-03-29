﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Info about chat invites exported by a certain admin.
/// See <a href="https://corefork.telegram.org/constructor/messages.exportedChatInvites" />
///</summary>
[TlObject(0xbdc62dcc)]
public sealed class TExportedChatInvites : IExportedChatInvites
{
    public uint ConstructorId => 0xbdc62dcc;
    ///<summary>
    /// Number of invites exported by the admin
    ///</summary>
    public int Count { get; set; }

    ///<summary>
    /// Exported invites
    ///</summary>
    public TVector<MyTelegram.Schema.IExportedChatInvite> Invites { get; set; }

    ///<summary>
    /// Info about the admin
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Count);
        writer.Write(Invites);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Count = reader.ReadInt32();
        Invites = reader.Read<TVector<MyTelegram.Schema.IExportedChatInvite>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}