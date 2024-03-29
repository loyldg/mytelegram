﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about chat invites generated by admins.
/// See <a href="https://corefork.telegram.org/constructor/chatAdminWithInvites" />
///</summary>
[TlObject(0xf2ecef23)]
public sealed class TChatAdminWithInvites : IChatAdminWithInvites
{
    public uint ConstructorId => 0xf2ecef23;
    ///<summary>
    /// The admin
    ///</summary>
    public long AdminId { get; set; }

    ///<summary>
    /// Number of invites generated by the admin
    ///</summary>
    public int InvitesCount { get; set; }

    ///<summary>
    /// Number of revoked invites
    ///</summary>
    public int RevokedInvitesCount { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(AdminId);
        writer.Write(InvitesCount);
        writer.Write(RevokedInvitesCount);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        AdminId = reader.ReadInt64();
        InvitesCount = reader.ReadInt32();
        RevokedInvitesCount = reader.ReadInt32();
    }
}