﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
///See <a href="https://core.telegram.org/method/account.getSecureValue" />
///</summary>
[TlObject(0x73665bc2)]
public sealed class RequestGetSecureValue : IRequest<TVector<MyTelegram.Schema.ISecureValue>>
{
    public uint ConstructorId => 0x73665bc2;
    public TVector<MyTelegram.Schema.ISecureValueType> Types { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        Types.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Types = br.Deserialize<TVector<MyTelegram.Schema.ISecureValueType>>();
    }
}