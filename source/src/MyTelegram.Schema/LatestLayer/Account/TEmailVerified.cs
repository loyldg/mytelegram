﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// The email was verified correctly.
/// See <a href="https://corefork.telegram.org/constructor/account.emailVerified" />
///</summary>
[TlObject(0x2b96cd1b)]
public sealed class TEmailVerified : IEmailVerified
{
    public uint ConstructorId => 0x2b96cd1b;
    ///<summary>
    /// The verified email address.
    ///</summary>
    public string Email { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Email);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Email = reader.ReadString();
    }
}