﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Toggle contact sign up notifications
/// See <a href="https://corefork.telegram.org/method/account.setContactSignUpNotification" />
///</summary>
[TlObject(0xcff43f61)]
public sealed class RequestSetContactSignUpNotification : IRequest<IBool>
{
    public uint ConstructorId => 0xcff43f61;
    ///<summary>
    /// Whether to disable contact sign up notifications
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool Silent { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Silent);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Silent = reader.Read();
    }
}
