﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// User is typing.
/// See <a href="https://corefork.telegram.org/constructor/sendMessageTypingAction" />
///</summary>
[TlObject(0x16bf744e)]
public sealed class TSendMessageTypingAction : ISendMessageAction
{
    public uint ConstructorId => 0x16bf744e;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}