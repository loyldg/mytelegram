﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Saves logs of application on the server.
/// See <a href="https://corefork.telegram.org/method/help.saveAppLog" />
///</summary>
[TlObject(0x6f02f748)]
public sealed class RequestSaveAppLog : IRequest<IBool>
{
    public uint ConstructorId => 0x6f02f748;
    ///<summary>
    /// List of input events
    ///</summary>
    public TVector<MyTelegram.Schema.IInputAppEvent> Events { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Events);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Events = reader.Read<TVector<MyTelegram.Schema.IInputAppEvent>>();
    }
}