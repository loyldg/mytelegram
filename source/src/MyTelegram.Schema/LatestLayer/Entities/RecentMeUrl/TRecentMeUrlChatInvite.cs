﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Recent t.me invite link to a chat
/// See <a href="https://corefork.telegram.org/constructor/recentMeUrlChatInvite" />
///</summary>
[TlObject(0xeb49081d)]
public sealed class TRecentMeUrlChatInvite : IRecentMeUrl
{
    public uint ConstructorId => 0xeb49081d;
    ///<summary>
    /// t.me URL
    ///</summary>
    public string Url { get; set; }

    ///<summary>
    /// Chat invitation
    /// See <a href="https://corefork.telegram.org/type/ChatInvite" />
    ///</summary>
    public MyTelegram.Schema.IChatInvite ChatInvite { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Url);
        writer.Write(ChatInvite);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Url = reader.ReadString();
        ChatInvite = reader.Read<MyTelegram.Schema.IChatInvite>();
    }
}