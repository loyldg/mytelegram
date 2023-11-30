﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a sent inline webview message
/// See <a href="https://corefork.telegram.org/constructor/webViewMessageSent" />
///</summary>
[TlObject(0xc94511c)]
public sealed class TWebViewMessageSent : IWebViewMessageSent
{
    public uint ConstructorId => 0xc94511c;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Message ID
    /// See <a href="https://corefork.telegram.org/type/InputBotInlineMessageID" />
    ///</summary>
    public MyTelegram.Schema.IInputBotInlineMessageID? MsgId { get; set; }

    public void ComputeFlag()
    {
        if (MsgId != null) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(MsgId); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { MsgId = reader.Read<MyTelegram.Schema.IInputBotInlineMessageID>(); }
    }
}