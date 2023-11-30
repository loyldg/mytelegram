﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/inputBotInlineMessageMediaWebPage" />
///</summary>
[TlObject(0xbddcc510)]
public sealed class TInputBotInlineMessageMediaWebPage : IInputBotInlineMessage
{
    public uint ConstructorId => 0xbddcc510;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool InvertMedia { get; set; }
    public bool ForceLargeMedia { get; set; }
    public bool ForceSmallMedia { get; set; }
    public bool Optional { get; set; }
    public string Message { get; set; }
    public TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    public string Url { get; set; }
    public MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }

    public void ComputeFlag()
    {
        if (InvertMedia) { Flags[3] = true; }
        if (ForceLargeMedia) { Flags[4] = true; }
        if (ForceSmallMedia) { Flags[5] = true; }
        if (Optional) { Flags[6] = true; }
        if (Entities?.Count > 0) { Flags[1] = true; }
        if (ReplyMarkup != null) { Flags[2] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Message);
        if (Flags[1]) { writer.Write(Entities); }
        writer.Write(Url);
        if (Flags[2]) { writer.Write(ReplyMarkup); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[3]) { InvertMedia = true; }
        if (Flags[4]) { ForceLargeMedia = true; }
        if (Flags[5]) { ForceSmallMedia = true; }
        if (Flags[6]) { Optional = true; }
        Message = reader.ReadString();
        if (Flags[1]) { Entities = reader.Read<TVector<MyTelegram.Schema.IMessageEntity>>(); }
        Url = reader.ReadString();
        if (Flags[2]) { ReplyMarkup = reader.Read<MyTelegram.Schema.IReplyMarkup>(); }
    }
}