﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/emailVerifyPurposeLoginChange" />
///</summary>
[TlObject(0x527d22eb)]
public class TEmailVerifyPurposeLoginChange : IEmailVerifyPurpose
{
    public uint ConstructorId => 0x527d22eb;


    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);

    }

    public void Deserialize(BinaryReader br)
    {

    }
}