﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/inputMessagesFilterMyMentions" />
///</summary>
[TlObject(0xc1f8e69a)]
public sealed class TInputMessagesFilterMyMentions : IMessagesFilter
{
    public uint ConstructorId => 0xc1f8e69a;


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