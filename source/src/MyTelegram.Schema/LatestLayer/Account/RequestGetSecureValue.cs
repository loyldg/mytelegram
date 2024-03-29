﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Get saved <a href="https://corefork.telegram.org/passport">Telegram Passport</a> document, <a href="https://corefork.telegram.org/passport/encryption#encryption">for more info see the passport docs »</a>
/// See <a href="https://corefork.telegram.org/method/account.getSecureValue" />
///</summary>
[TlObject(0x73665bc2)]
public sealed class RequestGetSecureValue : IRequest<TVector<MyTelegram.Schema.ISecureValue>>
{
    public uint ConstructorId => 0x73665bc2;
    ///<summary>
    /// Requested value types
    ///</summary>
    public TVector<MyTelegram.Schema.ISecureValueType> Types { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Types);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Types = reader.Read<TVector<MyTelegram.Schema.ISecureValueType>>();
    }
}
