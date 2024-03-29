﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Send phone call debug data to server
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.saveCallDebug" />
///</summary>
[TlObject(0x277add7e)]
public sealed class RequestSaveCallDebug : IRequest<IBool>
{
    public uint ConstructorId => 0x277add7e;
    ///<summary>
    /// Phone call
    /// See <a href="https://corefork.telegram.org/type/InputPhoneCall" />
    ///</summary>
    public MyTelegram.Schema.IInputPhoneCall Peer { get; set; }

    ///<summary>
    /// Debug statistics obtained from libtgvoip
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    public MyTelegram.Schema.IDataJSON Debug { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(Debug);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IInputPhoneCall>();
        Debug = reader.Read<MyTelegram.Schema.IDataJSON>();
    }
}
