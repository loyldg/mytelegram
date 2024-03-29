﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Used to download a JSON file that will contain all personal data related to features that do not have a specialized <a href="https://corefork.telegram.org/api/takeout">takeout method</a> yet, see <a href="https://corefork.telegram.org/api/takeout">here »</a> for more info on the takeout API.
/// See <a href="https://corefork.telegram.org/constructor/inputTakeoutFileLocation" />
///</summary>
[TlObject(0x29be5899)]
public sealed class TInputTakeoutFileLocation : IInputFileLocation
{
    public uint ConstructorId => 0x29be5899;


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