﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Reorder <a href="https://corefork.telegram.org/api/folders">folders</a>
/// See <a href="https://corefork.telegram.org/method/messages.updateDialogFiltersOrder" />
///</summary>
[TlObject(0xc563c1e4)]
public sealed class RequestUpdateDialogFiltersOrder : IRequest<IBool>
{
    public uint ConstructorId => 0xc563c1e4;
    ///<summary>
    /// New <a href="https://corefork.telegram.org/api/folders">folder</a> order
    ///</summary>
    public TVector<int> Order { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Order);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Order = reader.Read<TVector<int>>();
    }
}
