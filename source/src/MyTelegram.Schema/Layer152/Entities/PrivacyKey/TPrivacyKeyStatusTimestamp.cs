﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/privacyKeyStatusTimestamp" />
///</summary>
[TlObject(0xbc2eab30)]
public sealed class TPrivacyKeyStatusTimestamp : IPrivacyKey
{
    public uint ConstructorId => 0xbc2eab30;


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