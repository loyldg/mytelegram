﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Empty constructor, remove group photo.
/// See <a href="https://corefork.telegram.org/constructor/inputChatPhotoEmpty" />
///</summary>
[TlObject(0x1ca48f57)]
public sealed class TInputChatPhotoEmpty : IInputChatPhoto,IEmpty
{
    public uint ConstructorId => 0x1ca48f57;


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