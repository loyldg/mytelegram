﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Animated emoji reaction stickerset (contains animations to play when a user clicks on a given animated emoji)
/// See <a href="https://corefork.telegram.org/constructor/inputStickerSetAnimatedEmojiAnimations" />
///</summary>
[TlObject(0xcde3739)]
public sealed class TInputStickerSetAnimatedEmojiAnimations : IInputStickerSet
{
    public uint ConstructorId => 0xcde3739;


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