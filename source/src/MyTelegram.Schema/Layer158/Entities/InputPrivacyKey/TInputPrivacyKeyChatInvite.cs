﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/inputPrivacyKeyChatInvite" />
///</summary>
[TlObject(0xbdfb0426)]
public sealed class TInputPrivacyKeyChatInvite : IInputPrivacyKey
{
    public uint ConstructorId => 0xbdfb0426;


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