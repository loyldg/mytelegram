﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Data from an opened <a href="https://corefork.telegram.org/api/bots/webapps">reply keyboard bot web app</a> was relayed to the bot that owns it (bot side service message).
/// See <a href="https://corefork.telegram.org/constructor/messageActionWebViewDataSentMe" />
///</summary>
[TlObject(0x47dd8079)]
public sealed class TMessageActionWebViewDataSentMe : IMessageAction
{
    public uint ConstructorId => 0x47dd8079;
    ///<summary>
    /// Text of the <a href="https://corefork.telegram.org/constructor/keyboardButtonSimpleWebView">keyboardButtonSimpleWebView</a> that was pressed to open the web app.
    ///</summary>
    public string Text { get; set; }

    ///<summary>
    /// Relayed data.
    ///</summary>
    public string Data { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Text);
        writer.Write(Data);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Text = reader.ReadString();
        Data = reader.ReadString();
    }
}