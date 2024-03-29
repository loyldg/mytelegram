﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a bot logged in using the <a href="https://corefork.telegram.org/widgets/login">Telegram login widget</a>
/// See <a href="https://corefork.telegram.org/constructor/webAuthorization" />
///</summary>
[TlObject(0xa6f8f452)]
public sealed class TWebAuthorization : IWebAuthorization
{
    public uint ConstructorId => 0xa6f8f452;
    ///<summary>
    /// Authorization hash
    ///</summary>
    public long Hash { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    public long BotId { get; set; }

    ///<summary>
    /// The domain name of the website on which the user has logged in.
    ///</summary>
    public string Domain { get; set; }

    ///<summary>
    /// Browser user-agent
    ///</summary>
    public string Browser { get; set; }

    ///<summary>
    /// Platform
    ///</summary>
    public string Platform { get; set; }

    ///<summary>
    /// When was the web session created
    ///</summary>
    public int DateCreated { get; set; }

    ///<summary>
    /// When was the web session last active
    ///</summary>
    public int DateActive { get; set; }

    ///<summary>
    /// IP address
    ///</summary>
    public string Ip { get; set; }

    ///<summary>
    /// Region, determined from IP address
    ///</summary>
    public string Region { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Hash);
        writer.Write(BotId);
        writer.Write(Domain);
        writer.Write(Browser);
        writer.Write(Platform);
        writer.Write(DateCreated);
        writer.Write(DateActive);
        writer.Write(Ip);
        writer.Write(Region);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Hash = reader.ReadInt64();
        BotId = reader.ReadInt64();
        Domain = reader.ReadString();
        Browser = reader.ReadString();
        Platform = reader.ReadString();
        DateCreated = reader.ReadInt32();
        DateActive = reader.ReadInt32();
        Ip = reader.ReadString();
        Region = reader.ReadString();
    }
}