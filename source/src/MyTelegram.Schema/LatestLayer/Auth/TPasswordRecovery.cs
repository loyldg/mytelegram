﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Auth;

///<summary>
/// Recovery info of a <a href="https://corefork.telegram.org/api/srp">2FA password</a>, only for accounts with a <a href="https://corefork.telegram.org/api/srp#email-verification">recovery email configured</a>.
/// See <a href="https://corefork.telegram.org/constructor/auth.passwordRecovery" />
///</summary>
[TlObject(0x137948a5)]
public sealed class TPasswordRecovery : IPasswordRecovery
{
    public uint ConstructorId => 0x137948a5;
    ///<summary>
    /// The email to which the recovery code was sent must match this <a href="https://corefork.telegram.org/api/pattern">pattern</a>.
    ///</summary>
    public string EmailPattern { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(EmailPattern);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        EmailPattern = reader.ReadString();
    }
}