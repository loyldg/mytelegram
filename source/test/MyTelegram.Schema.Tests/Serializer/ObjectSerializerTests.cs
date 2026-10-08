using MyTelegram.Schema.E2e;
using MyTelegram.Schema.Upload;
using MyTelegram.Schema.Users;
using System.Reflection;
using Xunit.v3;

#pragma warning disable CS8618

namespace MyTelegram.Schema.Serializer;

public class TestM : IObject
{
    public uint ConstructorId => 0x73f1f8dc;

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        MsgId = buffer.ReadInt64();
        SeqNo = buffer.ReadInt32();
        Bytes = buffer.ReadInt32();
        Body = new byte[Bytes];
        buffer.TryCopyTo(Body);
        //reader.Advance(Bytes);
        buffer = buffer[Bytes..];
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(MsgId);
        writer.Write(SeqNo);
        writer.Write(Bytes);
        //Body.Serialize(writer);
    }

    public long MsgId { get; set; }

    public int SeqNo { get; set; }

    public int Bytes { get; set; }

    //public IObject Body { get; set; }
    public byte[] Body { get; set; }
}

[TlObject(0x73f1f8dc)]
// ReSharper disable once InconsistentNaming
public class TestMC : IRequest<IObject>
{
    public uint ConstructorId => 0x73f1f8dc;
    public TestM[] Messages { get; set; }

    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(Messages.Length);
        foreach (var containerMessage in Messages)
        {
            writer.Write(containerMessage);
        }
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //if (reader.TryReadLittleEndian(out int length))
        var length = buffer.ReadInt32();
        if (length > 0)
        {
            Messages = new TestM[length];
            for (int i = 0; i < length; i++)
            {
                var item = new TestM();
                item.Deserialize(ref buffer);
                Messages[i] = item;
            }
        }
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }
}

public class ObjectSerializerTests
{
    private readonly ITestOutputHelper _outputHelper;
    public ObjectSerializerTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        // Only need this code when tl object and serializer in different assembly
        SerializerObjectMappings.CreateConstructIdToTypeMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    //[Fact]
    //public void Deserialize_Property_Of_VectorOfTObjectTest()
    //{
    //    var bytes = "01 6D 5C F3 00 3C DF AF E9 25 10 6A 15 C4 B5 1C 0D 00 00 00 3B 70 D9 16 D1 CA 1E 00 00 00 00 00\r\n49 50 D0 09 3B 70 D9 16 92 AB 1E 00 00 00 00 00 49 50 D0 09 3B 70 D9 16 19 BF 1E 00 00 00 00 00\r\n49 50 D0 09 3B 70 D9 16 91 AB 1E 00 00 00 00 00 49 39 B9 ED 10 27 10 6A 3B 70 D9 16 49 B7 1E 00\r\n00 00 00 00 3F 70 8C 00 0F 27 10 6A 3B 70 D9 16 79 AF 1E 00 00 00 00 00 49 50 D0 09 3B 70 D9 16\r\n61 B3 1E 00 00 00 00 00 49 50 D0 09 3B 70 D9 16 A1 D2 1E 00 00 00 00 00 49 50 D0 09 3B 70 D9 16\r\nB9 CE 1E 00 00 00 00 00 49 50 D0 09 3B 70 D9 16 31 38 1F 00 00 00 00 00 49 50 D0 09 3B 70 D9 16\r\n01 40 1F 00 00 00 00 00 49 50 D0 09 3B 70 D9 16 D1 47 1F 00 00 00 00 00 49 50 D0 09 3B 70 D9 16\r\n19 3C 1F 00 00 00 00 00 49 50 D0 09".ToBytes();

    //    var obj = new TRpcResult();
    //    ReadOnlyMemory<byte> buffer = bytes.AsMemory();
    //    buffer = buffer.Slice(4);
    //    obj.Deserialize(ref buffer);
    //}

    [Fact]
    public void Deserialize_RpcResult_Test2()
    {
        var bytes =
            "01 6D 5C F3 0C E2 1C D7 0E DB 1E 6A 0D 81 5F 4E 40 42 AE 74 15 C4 B5 1C 02 00 00 00 D6 BF 90 4E\r\nC9 C7 03 00 C2 B8 74 44 58 0B 5A 47 FD 0A 2B 1F 0A 0E 80 7A 02 01 00 00 C9 C7 03 00 22 17 51 59\r\n91 AB 1E 00 00 00 00 00 22 17 51 59 92 AB 1E 00 00 00 00 00 15 DB 1E 6A 22 15 C3 E6 42 10 00 00\r\nCD A9 F0 85 01 08 01 00 2A A8 20 6E FA 33 AC 26 83 04 00 00 D0 4D 41 50 0D 44 65 73 6B 20 43 61\r\n6C 65 6E 64 61 72 00 00 0F 44 65 73 6B 43 61 6C 65 6E 64 61 72 2D 32 31 15 00 00 00 22 17 51 59\r\n92 AB 1E 00 00 00 00 00 15 C4 B5 1C 03 00 00 00 E2 51 52 56 00 00 00 00 0A 42 6C 61 63 6B 20 46\r\n6C 69 70 00 D8 C4 D4 8F 01 00 00 00 C1 5F 00 00 91 50 A9 48 C2 5E B2 95 80 B4 F5 BE 10 5E 8E 90\r\n8F CA DC 88 4E 9D 60 BB C3 35 98 06 E0 00 00 00 A0 25 94 67 17 61 70 70 6C 69 63 61 74 69 6F 6E\r\n2F 78 2D 74 67 73 74 69 63 6B 65 72 3B 4D 00 00 00 00 00 00 15 C4 B5 1C 02 00 00 00 41 4D 21 D8\r\n01 6A 00 00 83 29 02 B2 00 DC 46 08 4D 4D 06 5E 54 03 6D 65 49 4D 01 4D 4E 04 7C 44 4E 86 62 89\r\n6F 81 45 7E 82 46 04 4C 81 45 41 4A 80 4F 81 46 BD 48 03 86 09 49 07 8B 55 BA 4E 04 BD 4E 06 8C\r\n4B 9E 83 A8 43 8A 46 89 69 A2 6E A3 46 AB 9A AF B5 80 80 8F 81 90 81 8F 82 9F 84 AF 84 80 80 91\r\n69 A4 6D B1 49 A8 B1 AE B4 90 8D 87 02 48 89 03 A6 88 91 4B 87 06 4F 89 07 4E 87 06 5C 8F 01 6B\r\n96 06 43 93 44 AD 5D AD 60 8E C7 75 01 6D 00 00 80 00 00 00 80 00 00 00 7E 19 00 00 01 00 00 00\r\n15 C4 B5 1C 03 00 00 00 5C C1 37 6C 00 02 00 00 00 02 00 00 99 98 14 FD 00 00 00 00 04 F0 9F 97\r\n93 00 00 00 69 A2 E7 9D 1B 00 00 00 C0 36 4F 13 1C 3E B2 E7 2E CA F9 A5 68 00 59 15 13 41 6E 69\r\n6D 61 74 65 64 53 74 69 63 6B 65 72 2E 74 67 73 37 77 43 36 0E 00 00 00 E4 04 25 9F 0F 42 61 74\r\n74 6C 65 73 68 69 70 20 47 72 65 79 32 00 00 00 85 8C 8C 00 66 6C 6C 00 20 2A 2B 00 C4 CE CF 00\r\n37 77 43 36 0F 00 00 00 EA 85 70 4E 0D 53 68 6F 6F 74 69 6E 67 20 53 74 61 72 00 00 D8 C4 D4 8F\r\n01 00 00 00 85 69 00 00 80 EF 69 4B A8 EA 34 A2 88 EC 58 C2 10 EF CE 31 30 3B 23 30 42 BF A0 B1\r\n74 10 70 4A 47 00 00 00 1E 6C 6D 67 17 61 70 70 6C 69 63 61 74 69 6F 6E 2F 78 2D 74 67 73 74 69\r\n63 6B 65 72 B1 03 00 00 00 00 00 00 15 C4 B5 1C 02 00 00 00 41 4D 21 D8 01 6A 00 00 F9 0E 09 B0\r\n08 DC 4E 4A 63 70 60 46 08 82 4E 96 62 9D 6E 82 43 8C 4E 8A 52 49 52 73 A0 46 05 A2 52 84 7D 47\r\n47 05 54 4E 4E 87 02 7E 87 02 46 06 44 62 54 4C 04 A9 47 08 8A 88 A2 A4 A8 A3 9E 43 B9 65 89 01\r\n5E 9C 86 4F 88 08 4D 89 09 80 83 83 84 85 86 8B 8D 95 99 9E A6 B1 87 07 46 07 A6 47 05 AF 54 94\r\n5E B9 7A 86 07 F3 CC 25 08 A1 02 DC 5A 43 47 08 8C 49 07 4E 52 59 8F 72 A2 7D AA 59 8C 04 6B 90\r\n06 4B 8F 8C 96 9A 95 AC 42 A8 6F AA 47 09 AA 4F 80 5E 80 6D 80 F3 CC 1F 03 96 04 DC 48 81 50 81\r\n55 46 6A 73 89 09 4B 05 8E 02 4A 08 A1 85 B7 A9 9D 87 00 4B 8C 5E 8F 6E 93 4F 83 5F 86 6F 8B 53\r\n85 66 8C 79 8E F3 CC 12 05 95 09 DC 6C 44 49 47 02 85 49 02 8F 56 A1 69 B7 79 98 53 8B 09 47 01\r\n8D 01 4C 89 A8 47 06 87 06 4A 00 89 04 59 93 7A 86 05 49 01 86 07 00 00 60 8E C7 75 01 6D 00 00\r\n80 00 00 00 80 00 00 00 10 06 00 00 01 00 00 00 15 C4 B5 1C 03 00 00 00 5C C1 37 6C 00 02 00 00\r\n00 02 00 00 99 98 14 FD 02 00 00 00 04 F0 9F 8E 81 00 00 00 69 A2 E7 9D F9 FF 7F FF B5 05 73 6B\r\nE1 FC 53 79 A9 99 6A 07 68 00 59 15 13 41 6E 69 6D 61 74 65 64 53 74 69 63 6B 65 72 2E 74 67 73\r\n37 77 43 36 03 00 00 00 18 00 00 00 20 A1 07 00 9A 63 C0 B9 00 00 00 00 2A A8 20 6E FA 33 AC 26\r\n1B 00 00 00 C0 36 4F 13 F9 FF 7F FF B5 05 73 6B 20 2A 2B 00 15 C4 B5 1C 03 00 00 00 85 8C 8C 00\r\nC4 CE CF 00 66 6C 6C 00 FA 00 00 00 22 17 51 59 91 AB 1E 00 00 00 00 00 2C 01 00 00 00 00 00 00\r\n00 00 00 00 1E 0D 12 00 01 00 00 00 15 C4 B5 1C 00 00 00 00 15 C4 B5 1C 00 00 00 00 15 DB 1E 6A\r\n00 00 00 00"
                .ToBytes();


        var bytes2 =
            "15 C4 B5 1C 03 00 00 00 85 8C 8C 00\r\nC4 CE CF 00 66 6C 6C 00 FA 00 00 00 22 17 51 59 91 AB 1E 00 00 00 00 00 2C 01 00 00 00 00 00 00\r\n00 00 00 00 1E 0D 12 00 01 00 00 00 15 C4 B5 1C 00 00 00 00 15 C4 B5 1C 00 00 00 00 15 DB 1E 6A\r\n00 00 00 00"
                .ToBytes();
        ReadOnlyMemory<byte> buffer2 = bytes2;
        var obj = buffer2.ToTObject<IObject>();

        ReadOnlyMemory<byte> buffer = bytes;
        buffer = buffer.Slice(4);
        var last28 = buffer[^28..];

        var rpcResult = new TRpcResult();

        rpcResult.Deserialize(ref buffer);
    }

    [Fact]
    public void DeserializeBlockchainData_Test()
    {
        var bytes =
            "B63D9A63761A4B5A6E932F9F2693586A4865EB35FAE2E44DD4C19391D902623D897947F70AEBCEBE9121CA55635C3A27A0AAB36E1C3C828EE6D7D81317E82A474EC1BD05010000000000000000000000000000000000000000000000000000000000000000000000020000004671F12C8475DC1D010000001F97F31831381F0000000000C1B43BCA1EBF5435F9E1BD78DBF8F1AEEDD3E5269D66D52846DC9B1AB5DAE55E03000000000000000300000058217A987F7E848A41981CDBAFD8F98195A60E81DC9E878AB6C7EE38722792E6B93A5B046904C9A2403D215B4BE42B675FA8FE96A454FF45EF3FCECF1B3397566293F4B0F83F59E32E0829EDB391CCFF5D41516BC21B1F9E47BAAB439743CD527058AFD4DC8364B9A50000000100000031381F000000000001000000205F6113E62FFEACAE142A559B3A986E25584737F04744041F6C93D815A55ED09900000000000000E679B6D600000000DF3F619804A92FDB4057192DC43DD748EA778ADC52BC498CE80524C014B81119C1B43BCA1EBF5435F9E1BD78DBF8F1AEEDD3E5269D66D52846DC9B1AB5DAE55E".ToBytes();
        var block = new TBlock();
        ReadOnlyMemory<byte> buffer = bytes;
        buffer = buffer.Slice(4);
        block.Deserialize(ref buffer);
    }

    [Fact]
    public void DeserializeReadOnlyMemory_Test()
    {
        var settings = new TCodeSettings
        {
            LogoutTokens = new TVector<ReadOnlyMemory<byte>>(new byte[3] { 5, 6, 7 }, new byte[4] { 1, 2, 3, 4 })
        };
        var writer = new ArrayBufferWriter<byte>();
        settings.Serialize(writer);
        //SerializerFactory.CreateSerializer<ReadOnlyMemory<byte>>();
        var bytes = writer.WrittenMemory.Slice(4);
        
        var obj = new TCodeSettings();
        obj.Deserialize(ref bytes);

        obj.LogoutTokens.ShouldNotBeNull();
        obj.LogoutTokens.Count.ShouldBe(settings.LogoutTokens.Count);
        obj.LogoutTokens[0].Span.SequenceEqual(settings.LogoutTokens[0].Span).ShouldBeTrue();
        obj.LogoutTokens[1].Span.SequenceEqual(settings.LogoutTokens[1].Span).ShouldBeTrue();
    }

    //public void Deserialize_GzipPackedData()
    //{
    //    var packedData =
    //        "1F8B0800000000000203ED52C14AC340100DA8979CBCEA41E6660B16D24DB2C9068A48295A54E8C50F58B243134C37259B147BF33BFC0F41D49357F10FD41FF0EA518ADB24F550417A113C382CCC2E33EFCD9BE55D3D6FCC2E0DC3F8B8BBDF7DD8BEE9DEEA7BF638B97E7A79DD9AC1BA312E540461C4A5C40446A8141F62E05B65B8D4B24800A47CB4CD3A43AC2095492C714FA6201105E429CC79BE3A94E689530949ACF280982BCF20A642294A2E103CE773E24261162CCA2B31B549A5B6CC3FAAAD3A96D48A588D538570C2A798A118E8D6D36A5237439EA3E8CB1C8719CF35A63741999BF396851ACD1E2C11F74587508710E6B7A93E4C87CB1C1860363A28F2E818A77D11B45CA6F760D47789DE845A9E4D2C68B46C8F32DB7219B35DD76933DBF758137A176152083CD33FA3918D261CA2C42C0E21994BEE108B020F433DFC88AB8ABEE36BB46751CF716C9F3A8C52DB3675158635B296BA906EFEDBE22FDBE29B05EA1FFE0527BCAFED9F476F3B9B9F00BC256B48040000"
    //            .ToBytes();

    //}

    //    [Fact]
    //    public void Deserialize_Test3333()
    //    {
    //        var bytes =
    //            @"0D-0D-9B-DA-AE-00-00-00-A9-5E-CD-C1-02-00-00-00-01-00-00-00-10-69-50-68-6F-6E-65-20-53-69-6D-75-6C-61-
    //74-6F-72-00-00-00-04-31-37-2E-32-00-00-00-0F-31-30-2E-38-2E-31-20-28-31-30-30-30-30-29-20-05-65-6E-2D-43-4E-00-
    //00-03-69-6F-73-07-7A-68-2D-68-61-6E-73-9D-D4-C1-99-15-C4-B5-1C-02-00-00-00-D9-1B-DE-C0-09-74-7A-5F-6F-66-66-73-
    //65-74-00-00-A4-DF-E0-2B-00-00-00-00-00-20-DC-40-D9-1B-DE-C0-08-62-75-6E-64-6C-65-49-64-00-00-00-7A-76-1E-B7-14-
    //70-68-2E-74-65-6C-65-67-72-61-2E-54-65-6C-65-67-72-61-70-68-00-00-00".ToBytes();
    //        var s = new RequestInvokeWithLayer();
    //        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(bytes));
    //        buffer.ReadUInt32();

    //        s.Deserialize(ref reader);
    //    }

    //    [Fact]
    //    public void Deserialize_E2EBlock()
    //    {
    //        var bytes = @"B6 3D 9A 63 D8 FD F1 F9 CA 88 71 AD FF 46 D8 65 A4 BE 18 8D 7C 6F 9E 02 F3 8C BD 8C 84 5E DA A4
    //A5 04 10 42 D3 97 33 E1 1B 5B 23 09 6C DC 02 BB F3 40 3C 86 C2 F3 85 C8 4C F0 EB 7A C6 40 89 FE
    //10 E3 B5 0F 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
    //00 00 00 00 00 00 00 00 02 00 00 00 46 71 F1 2C 84 75 DC 1D 01 00 00 00 1F 97 F3 18 E9 C6 1E 00
    //00 00 00 00 4F B0 17 17 E3 15 CB 63 CE 14 76 F5 39 20 A7 A6 A5 38 15 F7 F8 87 58 AC DE 75 EB A8
    //FB F6 10 16 03 00 00 00 00 00 00 00 03 00 00 00 58 21 7A 98 7F 7E 84 8A C2 4E DA C9 13 22 FA B0
    //EC D8 02 C9 66 3D 59 10 69 D2 4C 80 E4 73 26 C0 51 9C 59 9F 61 47 E1 E7 40 6D A1 9A 6C 2A 5F 1B
    //84 69 39 D4 07 B7 69 7F 5B 46 B6 9C E4 DB 8D C6 8A 10 13 67 9D 29 94 5A 83 F0 00 FF 1B 82 D5 D7
    //29 05 9D F8 66 64 5B DB EC D4 DA 3D 9A 5E 84 6F 4B 7B CA 72 4F E2 AC 86 0E 00 00 00 01 00 00 00
    //E9 C6 1E 00 00 00 00 00 01 00 00 00 20 A3 A4 D8 24 9E 10 99 67 D9 BC 43 64 36 84 0A 1F 3C EF 33
    //AD 84 CB BB 56 EA FE 3A 75 7B 48 72 D9 00 00 00 00 00 00 00 E6 79 B6 D6 00 00 00 00 DF 3F 61 98
    //04 A9 2F DB 40 57 19 2D C4 3D D7 48 EA 77 8A DC 52 BC 49 8C E8 05 24 C0 14 B8 11 19 4F B0 17 17
    //E3 15 CB 63 CE 14 76 F5 39 20 A7 A6 A5 38 15 F7 F8 87 58 AC DE 75 EB A8 FB F6 10 16".ToBytes();
    //        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(bytes));
    //        ReadOnlyMemory<byte> buffer = bytes;
    //        buffer.ReadInt32();
    //        var s = new MyTelegram.Schema.E2e.TBlock();
    //        s.Deserialize(ref buffer);
    //    }

    [Fact]
    public void Deserialize_Test2222()
    {
        var bytes =
                            @"0D-0D-9B-DA-AE-00-00-00-A9-5E-CD-C1-02-00-00-00-01-00-00-00-10-69-50-68-6F-6E-65-20-53-69-6D-75-6C-61-74-6F-72-
                00-00-00-04-31-37-2E-32-00-00-00-0F-31-30-2E-38-2E-31-20-28-31-30-30-30-30-29-20-05-65-6E-2D-43-4E-00-00-03-69-6F-73-02-65-
                6E-00-9D-D4-C1-99-15-C4-B5-1C-02-00-00-00-D9-1B-DE-C0-08-62-75-6E-64-6C-65-49-64-00-00-00-7A-76-1E-B7-14-70-68-2E-74-65-6C-
                65-67-72-61-2E-54-65-6C-65-67-72-61-70-68-00-00-00-D9-1B-DE-C0-09-74-7A-5F-6F-66-66-73-65-74-00-00-A4-DF-E0-2B-00-
                00-00-00-00-20-DC-40-F7-02-E2-C0"
                .ToBytes();

        var s = new RequestInitConnection();
        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(bytes));
        ReadOnlyMemory<byte> buffer = bytes;
        buffer.ReadUInt32();
        buffer.ReadInt32();
        buffer.ReadUInt32();

        s.Deserialize(ref buffer);

    }

    [Fact]
    public void Deserialize_Test227()
    {
        var bytes = @"DC F8 F1 73 08 00 00 00 00 FC 89 1E CA DB DD 65 45 00 00 00 A8 00 00 00 0D 0D 9B DA AE 00 00 00
A9 5E CD C1 02 00 00 00 01 00 00 00 10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00
04 31 37 2E 32 00 00 00 0F 31 30 2E 38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00
03 69 6F 73 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49
64 00 00 00 7A 76 1E B7 14 70 68 2E 74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00
D9 1B DE C0 09 74 7A 5F 6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 F7 02 E2 C0
00 50 BC 1E CA DB DD 65 47 00 00 00 A8 00 00 00 0D 0D 9B DA AE 00 00 00 A9 5E CD C1 02 00 00 00
01 00 00 00 10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00 04 31 37 2E 32 00 00 00
0F 31 30 2E 38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00 03 69 6F 73 02 65 6E 00
9D D4 C1 99 15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49 64 00 00 00 7A 76 1E B7
14 70 68 2E 74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00 D9 1B DE C0 09 74 7A 5F
6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 F7 02 E2 C0 00 5C E4 1E CA DB DD 65
49 00 00 00 B4 00 00 00 0D 0D 9B DA AE 00 00 00 A9 5E CD C1 02 00 00 00 01 00 00 00 10 69 50 68
6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00 04 31 37 2E 32 00 00 00 0F 31 30 2E 38 2E 31 20
28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00 03 69 6F 73 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C
02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49 64 00 00 00 7A 76 1E B7 14 70 68 2E 74 65 6C 65
67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00 D9 1B DE C0 09 74 7A 5F 6F 66 66 73 65 74 00 00
A4 DF E0 2B 00 00 00 00 00 20 DC 40 3B 40 A9 9D 00 00 00 00 00 00 00 00 00 00 00 00 00 6C 03 1F
CA DB DD 65 4B 00 00 00 B0 00 00 00 0D 0D 9B DA AE 00 00 00 A9 5E CD C1 02 00 00 00 01 00 00 00
10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00 04 31 37 2E 32 00 00 00 0F 31 30 2E
38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00 03 69 6F 73 02 65 6E 00 9D D4 C1 99
15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49 64 00 00 00 7A 76 1E B7 14 70 68 2E
74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00 D9 1B DE C0 09 74 7A 5F 6F 66 66 73
65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 A9 AA F1 04 00 00 00 00 00 00 00 00 00 DC 1F 1F
CA DB DD 65 4D 00 00 00 A8 00 00 00 0D 0D 9B DA AE 00 00 00 A9 5E CD C1 02 00 00 00 01 00 00 00
10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00 04 31 37 2E 32 00 00 00 0F 31 30 2E
38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00 03 69 6F 73 02 65 6E 00 9D D4 C1 99
15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49 64 00 00 00 7A 76 1E B7 14 70 68 2E
74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00 D9 1B DE C0 09 74 7A 5F 6F 66 66 73
65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 6B 18 F9 C4 00 A8 3B 1F CA DB DD 65 4F 00 00 00
A8 00 00 00 0D 0D 9B DA AE 00 00 00 A9 5E CD C1 02 00 00 00 01 00 00 00 10 69 50 68 6F 6E 65 20
53 69 6D 75 6C 61 74 6F 72 00 00 00 04 31 37 2E 32 00 00 00 0F 31 30 2E 38 2E 31 20 28 31 30 30
30 30 29 20 05 65 6E 2D 43 4E 00 00 03 69 6F 73 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 02 00 00 00
D9 1B DE C0 08 62 75 6E 64 6C 65 49 64 00 00 00 7A 76 1E B7 14 70 68 2E 74 65 6C 65 67 72 61 2E
54 65 6C 65 67 72 61 70 68 00 00 00 D9 1B DE C0 09 74 7A 5F 6F 66 66 73 65 74 00 00 A4 DF E0 2B
00 00 00 00 00 20 DC 40 88 71 8B 65 00 28 64 1F CA DB DD 65 51 00 00 00 AC 00 00 00 0D 0D 9B DA
AE 00 00 00 A9 5E CD C1 02 00 00 00 01 00 00 00 10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F
72 00 00 00 04 31 37 2E 32 00 00 00 0F 31 30 2E 38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D
43 4E 00 00 03 69 6F 73 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E
64 6C 65 49 64 00 00 00 7A 76 1E B7 14 70 68 2E 74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70
68 00 00 00 D9 1B DE C0 09 74 7A 5F 6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40
2F F4 80 DA 00 00 00 00 00 A0 96 1F CA DB DD 65 53 00 00 00 AC 00 00 00 0D 0D 9B DA AE 00 00 00
A9 5E CD C1 02 00 00 00 01 00 00 00 10 69 50 68 6F 6E 65 20 53 69 6D 75 6C 61 74 6F 72 00 00 00
04 31 37 2E 32 00 00 00 0F 31 30 2E 38 2E 31 20 28 31 30 30 30 30 29 20 05 65 6E 2D 43 4E 00 00
03 69 6F 73 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 02 00 00 00 D9 1B DE C0 08 62 75 6E 64 6C 65 49
64 00 00 00 7A 76 1E B7 14 70 68 2E 74 65 6C 65 67 72 61 2E 54 65 6C 65 67 72 61 70 68 00 00 00
D9 1B DE C0 09 74 7A 5F 6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 FD A9 CF AB
00 00 00 00".ToBytes();
        //var s = new ObjectSerializer<TMsgContainer>();
        ReadOnlyMemory<byte> buffer = bytes;
        buffer.ReadUInt32();
        var s = new TestMC();
        s.Deserialize(ref buffer);
        s.Messages.Length.ShouldBe(8);
        TestOutputHelper h = new TestOutputHelper();

        var index = 0;

        foreach (var item in s.Messages)
        {
            try
            {
                //h.WriteLine($"{BitConverter.ToUInt32(item.Body):x2}");
                //_outputHelper.WriteLine($"#0x{BitConverter.ToUInt32(item.Body):x2}");
                //var r2 = new SequenceReader<byte>(new ReadOnlySequence<byte>(item.Body));
                ReadOnlyMemory<byte> r2 = bytes;
                r2.ReadUInt32();

                var invokeWithLayer = new RequestInvokeWithLayer();
                invokeWithLayer.Deserialize(ref r2);
                _outputHelper.WriteLine(System.Text.Json.JsonSerializer.Serialize(invokeWithLayer.Query));
            }
            catch (Exception ex)
            {
                _outputHelper.WriteLine($"Error:{index}");
                _outputHelper.WriteLine(ex.ToString());
            }

            index++;
        }
        Console.WriteLine();
    }

    [Fact]
    public void Deserialize_TVector_Of_IObject_As_IObject_Test()
    {
        var obj = new TVector<ISubObject>
        {
            new SubObject
            {
                Id = 1
            }
        };

        var writer = new ArrayBufferWriter<byte>();
        obj.Serialize(writer);

        var actualValue = Deserialize<IObject>(writer.WrittenSpan.ToArray());
        var vector = actualValue as TVector<ISubObject>;
        var firstItem = vector?.FirstOrDefault() as SubObject;

        vector.ShouldNotBeNull();
        vector.Count.ShouldBe(1);
        firstItem.ShouldNotBeNull();
        firstItem.Id.ShouldBe(1);
    }

    [Fact]
    public void Serialize_ResPQ_Test()
    {
        var expectedValue =
            "6324160500000000000000000000000000000000000000000000000000000000000000000817ED48941A08F98100000015C4B51C0200000001000000000000000200000000000000"
                .ToBytes();
        var obj = new TResPQ
        {
            Pq = new byte[] { 0x17, 0xED, 0x48, 0x94, 0x1A, 0x08, 0xF9, 0x81 },
            ServerNonce = new byte[16],
            Nonce = new byte[16],
            ServerPublicKeyFingerprints = new TVector<long> { 1, 2 }
        };

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_RequestGetFile_Test()
    {
        var expectedValue =
            "BE 35 53 BE 02 00 00 00 84 75 D0 BA 71 2F 18 DC 71 56 35 99 59 F5 E5 6C 49 E3 17 6D 10 BA 9B B2\r\n46 08 08 AD 4B 82 F3 22 A1 0B 0D 9C 06 00 00 00 00 00 00 00 00 00 74 00 00 00 00 00 00 00 02 00"
                .ToBytes();
        var obj = new RequestGetFile
        {
            CdnSupported = true,
            Offset = 7602176,
            Limit = 131072,
            Location = new TInputDocumentFileLocation
            {
                AccessHash = 7861001579097617753,
                FileReference = "BA 9B B2 46 08 08 AD 4B 82 F3 22 A1 0B 0D 9C 06".ToBytes(),
                Id = BitConverter.ToInt64(BitConverter.GetBytes(11039825108592504689)),
                ThumbSize = ""
            }
        };
        //using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);
        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);

        //obj.Serialize(bw);

        var bytes = obj.ToBytes();

        bytes.ShouldBeEquivalentTo(expectedValue);
        //writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_RequestGetFile_Test()
    {
        var value =
            "BE 35 53 BE 02 00 00 00 84 75 D0 BA 71 2F 18 DC 71 56 35 99 59 F5 E5 6C 49 E3 17 6D 10 BA 9B B2\r\n46 08 08 AD 4B 82 F3 22 A1 0B 0D 9C 06 00 00 00 00 00 00 00 00 00 74 00 00 00 00 00 00 00 02 00"
                .ToBytes();
        var obj = new RequestGetFile
        {
            CdnSupported = true,
            Offset = 7602176,
            Limit = 131072,
            Location = new TInputDocumentFileLocation
            {
                AccessHash = 7861001579097617753,
                FileReference = "BA 9B B2 46 08 08 AD 4B 82 F3 22 A1 0B 0D 9C 06".ToBytes(),
                Id = BitConverter.ToInt64(BitConverter.GetBytes(11039825108592504689)),
                ThumbSize = ""
            }
        };
        //obj.ComputeFlag();
        //var stream = new MemoryStream();
        //var br = new BinaryReader(stream);
        ////var buffer = new ReadOnlySequence<byte>(value);

        //var actualObj = br.Deserialize<RequestGetFile>();
        var actualObj = value.ToTObject<RequestGetFile>();

        //actualObj.ShouldBeEquivalentTo(obj);
        actualObj.CdnSupported.ShouldBe(obj.CdnSupported);
        actualObj.Offset.ShouldBe(obj.Offset);
        actualObj.Limit.ShouldBe(obj.Limit);
        actualObj.Location.ShouldNotBeNull();
        //actualObj.Location.ShouldBeEquivalentTo(obj.Location);
    }

    //[Fact]
    //public void Serialize_Test2()
    //{
    //    var obj = new RequestInvokeWithLayer
    //    {
    //        Layer = 148,
    //        Query = new RequestInitConnection
    //        {
    //            ApiId = 17349,
    //            DeviceModel = "Server",
    //            SystemVersion = "Windows 10",
    //            AppVersion = "4.3.1 x64",
    //            SystemLangCode = "en-US",
    //            LangPack = "tdesktop",
    //            LangCode = "en",
    //            Params = new TJsonObject
    //            {
    //                Value = new TVector<IJSONObjectValue>(new TJsonObjectValue
    //                {
    //                    Key = "tz_offset",
    //                    Value = new TJsonNumber
    //                    {
    //                        Value = 28800
    //                    }
    //                })
    //            },
    //            Query = new RequestGetStickerSet
    //            {
    //                Stickerset = new TInputStickerSetAnimatedEmoji(),
    //                Hash = 0
    //            }
    //        },
    //    };
    //    var expectedValue = "0D 0D 9B DA 94 00 00 00 A9 5E CD C1 02 00 00 00 C5 43 00 00 06 53 65 72 76 65 72 00 0A 57 69 6E\r\n64 6F 77 73 20 31 30 00 09 34 2E 33 2E 31 20 78 36 34 00 00 05 65 6E 2D 55 53 00 00 08 74 64 65\r\n73 6B 74 6F 70 00 00 00 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 01 00 00 00 D9 1B DE C0 09 74 7A 5F\r\n6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 2C 56 28 66 B5 75 72 99".ToBytes();
    //    //var serializer = new ObjectSerializer<IObject>();
    //    //using var writer = new ArrayPoolBufferWriter<byte>();

    //    //serializer.Serialize(obj, writer);
    //    //var a = writer.WrittenSpan.ToArray().ToHexString();

    //    //writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);

    //    SerializeTest(obj,expectedValue);
    //}

    //[Fact]
    //public void Deserialize_Test3()
    //{
    //    var value =
    //        "BE 35 53 BE 02 00 00 00 84 75 D0 BA 71 2F 18 DC 71 56 35 99 59 F5 E5 6C 49 E3 17 6D 10 BA 9B B2\r\n46 08 08 AD 4B 82 F3 22 A1 0B 0D 9C 06 00 00 00 30 3B FD 19 39 C1 9A D9 72 8D FB C3 9A F6 BE 0E"
    //            .ToBytes();
    //    //var buffer = new ReadOnlySequence<byte>(value);
    //    var serializer = new ObjectSerializer<IObject>();
    //    var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));

    //    var obj = serializer.Deserialize(ref reader);

    //    obj.ShouldNotBeNull();
    //}

    [Fact]
    public void Deserialize_Test2()
    {
        var value =
            ("0D 0D 9B DA 94 00 00 00 A9 5E CD C1 02 00 00 00 C5 43 00 00 06 53 65 72 76 65 72 00 0A 57 69 6E " +
            "64 6F 77 73 20 31 30 00 09 34 2E 33 2E 31 20 78 36 34 00 00 05 65 6E 2D 55 53 00 00 08 74 64 65 " +
            "73 6B 74 6F 70 00 00 00 02 65 6E 00 9D D4 C1 99 15 C4 B5 1C 01 00 00 00 D9 1B DE C0 09 74 7A 5F " +
            "6F 66 66 73 65 74 00 00 A4 DF E0 2B 00 00 00 00 00 20 DC 40 2C 56 28 66 B5 75 72 99")
                .ToBytes();
        var serializer = new ObjectSerializer<IObject>();
        ReadOnlyMemory<byte> buffer = value;
        var obj = serializer.Deserialize(ref buffer);

        obj.ShouldNotBeNull();
    }

    [Fact]
    public void Deserialize_Test3()
    {
        var value = @"84 09 E3 F1 86 5B 77 47 83 29 AC 9B 62 A6 18 6A 2E B3 C3 23 54 B6 43 66 FD 31 F9 FC E7 7E 81 99
08 91 95 6E AE 9F F6 F5 54 62 33 14 7F 75 E3 B3 7D 5A ED 80 03 33 0C 00 00 00 00 00 00 00 00 00
FE FF 00 00 E2 7D BB 37 D0 D1 4F D0 C7 F4 4A 1D AB 32 82 6F DF 0E 4E DC F5 73 AE 8D BE A1 40 85
66 00 87 71 5C F6 2B 48 C7 13 E9 0C 3C 2E 73 43 20 53 C1 3A C1 64 AA 6E BD 2D 08 EB 02 45 36 24
66 FD 39 DB 4F 13 9A B3 43 CC B8 E0 27 A1 D4 9D A1 C2 83 0C E5 AE 48 FF 9F DB 18 36 2E D3 35 84
AA FA 87 DA 16 97 4D 29 71 E2 3F 41 87 F4 79 D8 8F F0 37 8A 48 10 6E 01 BE D3 39 A0 EC B3 0D C1
18 3C 5B C4 4A BD E6 A5 77 DC 2D 81 AB 8C 8D 02 1E 8D 2F 48 B5 01 04 B7 CD 87 CD 96 01 2F 93 C0
77 D8 EF E7 D5 4C D2 B6 16 41 34 5C 07 53 B3 BF 71 39 40 F5 22 51 E7 4C AE C5 BD 8D 74 3E CC BA
67 55 0C 6D 1C 37 46 9F C0 FE AF 02 0D 16 32 6F 95 57 22 60 89 68 72 EE AB A9 61 24 65 C3 39 98
8C 47 EC 50 E4 6D 41 2A 11 41 4B 2E F3 D6 51 E6 38 0E D4 A0 4F 14 79 FF D0 A6 D8 D4 D4 6A 3F 87
F2 60 8B 00 CC 39 62 1A F1 0A AC 25 A6 C7 5F 5D".ToBytes();
        var serializer = new ObjectSerializer<IObject>();
        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value.AsSpan().Slice(20).ToArray()));
        ReadOnlyMemory<byte> buffer = value.AsMemory().Slice(20);
        var obj = serializer.Deserialize(ref buffer);
        var consumed = value.Length - buffer.Length;
        consumed.ShouldBe(value.Length - 12);
        //reader.Consumed.ShouldBe(value.Length - 12 - 20);
    }

    //public void D
    // 0x48a5910d15c4b51c010000004ca5e8dd81841e00000000000
    //48a5910d15c4b51c010000004ca5e8dd81841e0000000000b0bbd1de6b2f5bda

    //[Fact]
    //public void Deserialize_TVector_Of_IUpdates()
    //{
    //    var bytes = Convert.FromBase64String(
    //        "FcS1HAEAAADZBLpi4G4ROAABAAARJwAAIhdRWYGEHgAAAAAAHjelogNAt0O6AAAAXNDIZAV3YWZhZgAAEScAAAEAAAA=");
    //    var obj = bytes.ToTObject<TVector<IUpdate>>();

    //    obj.Count.ShouldBe(1);
    //}

    [Fact]
    public void Deserialize_Object_Contains_TVector_Of_IObject()
    {
        var value = "48a5910d15c4b51c01000000c65811f281841e0000000000b0bbd1de6b2f5bda".ToBytes();
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<RequestGetUsers>();

        //var actualObj = serializer.Deserialize(ref buffer);

        var serializer = new ObjectSerializer<RequestGetUsers>();
        ReadOnlyMemory<byte> buffer = value;

        var actualObj = serializer.Deserialize(ref buffer);

        actualObj.Id.Count.ShouldBe(1);
    }

    [Fact]
    public void Deserialize_Object_Contains_Nullable_Property()
    {
        var value = "0500000003000000B57572990A746573742076616C756500".ToBytes();
        var expectedValue =
            new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value", IntValue = null };
        expectedValue.ComputeFlag();
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TestObjectWithNullableProperty>();

        //var actualObject = serializer.Deserialize(ref buffer);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_SubObject()
    {
        var value = "040000000200000001000000".ToBytes();
        var expectedValue = new TestObjectWithSubObject { SubObject = new SubObject { Id = 1 } };
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TestObjectWithSubObject>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_SubObject2()
    {
        var value = "04000000030000000100000000000000".ToBytes();
        var expectedValue = new TestObjectWithSubObject { SubObject = new SubObject2 { Id = 1L } };
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TestObjectWithSubObject>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_TVector_Of_TLObject_Interface()
    {
        var value = @"0600000015C4B51C00000000".ToBytes();
        var expectedValue = new TestObjectWithTVectorOfInterface { SubObjects = new TVector<ISubObject>() };
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<IObject>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_RpcResult()
    {
        var value =
            "016D5CF363000000000000000600000015C4B51C020000000200000001000000230000000000000015C4B51C01000000240000000200000015C4B51C010000000431313131000000"
                .ToBytes();
        var expectedValue = new TRpcResult
        {
            ReqMsgId = 99,
            Result = new TestObjectWithTVectorOfInterface
            {
                SubObjects = new TVector<ISubObject>(new SubObject { Id = 1 },
                    new SubObject3WithNullableProperty
                    {
                        Level2Vector =
                            new TVector<ILevel2SubObject>(new Level2SubObject
                            {
                                Id = 2,
                                Level2SubVector = new TVector<string>("1111")
                            }),
                        Level2Vector2 = null // new TVector<ILevel2SubObject>()
                    })
            }
        };
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TRpcResult>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_Test()
    {
        var value = "01000000010000000974657374206E616D650000".ToBytes();
        var expectedValue = new TestObject { TestId = 1, Name = "test name" };
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TestObject>();

        //var actualValue = serializer.Deserialize(ref buffer);

        //actualValue.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_Empty_TLObject_Interface()
    {
        var value = "15C4B51C00000000".ToBytes();
        var expectedValue = new TVector<ISubObject>();
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TVector<ISubObject>>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        //var serializer = new ObjectSerializer<TVector<ISubObject>>();
        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));

        //var actualObj = serializer.Deserialize(ref reader);

        //actualObj.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    private void DeserializeTest<TExpectedValue>(byte[] value, TExpectedValue expectedValue)
    {
        var serializer = new ObjectSerializer<TExpectedValue>();
        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));
        ReadOnlyMemory<byte> buffer = value;
        var actualObj = serializer.Deserialize(ref buffer);

        actualObj.ShouldBeEquivalentTo(expectedValue);
    }

    private TExpectedValue Deserialize<TExpectedValue>(byte[] value)
    {
        var serializer = new ObjectSerializer<TExpectedValue>();
        //var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));
        ReadOnlyMemory<byte> buffer = value;
        var actualObj = serializer.Deserialize(ref buffer);

        return actualObj;
    }

    [Fact]
    public void Deserialize_TVector_Of_SimpleType()
    {
        var value = "15C4B51C0500000001000000000000000200000000000000030000000000000004000000000000000500000000000000"
            .ToBytes();
        var expectedValue = new TVector<long>(1,
            2,
            3,
            4,
            5);
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_TLObject()
    {
        var value = "15C4B51C010000000500000003000000B57572990A746573742076616C756500".ToBytes();
        var obj = new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" };
        obj.ComputeFlag();
        var expectedValue = new TVector<TestObjectWithNullableProperty>(obj);
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TVector<TestObjectWithNullableProperty>>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_TLObject_Interface()
    {
        var value = "15C4B51C020000000200000001000000030000000200000000000000".ToBytes();
        var expectedValue = new TVector<ISubObject>(new SubObject { Id = 1 }, new SubObject2 { Id = 2 });
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        //var buffer = new ReadOnlySequence<byte>(value);
        //var serializer = new ObjectSerializer<TVector<ISubObject>>();

        //var actualObject = serializer.Deserialize(ref buffer);

        //actualObject.ShouldBeEquivalentTo(expectedValue);

        DeserializeTest(value, expectedValue);
    }

    [Fact]
    public void Serialize_Non_IObject_Throws_Exception()
    {
        var serializer = new ObjectSerializer<TestNoneIObject>();
        using var writer = new ArrayPoolBufferWriter<byte>();

        Assert.Throws<NotSupportedException>(() =>
            serializer.Serialize(new TestNoneIObject(), writer));
    }

    [Fact]
    public void Serialize_Object_Contains_Flag_Property()
    {
        //                        "050000000403000000000000B57572990A746573742076616C756500"
        var expectedValue = "0500000003000000B57572990A746573742076616C756500".ToBytes();

        var obj = new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    private void SerializeTest<TData>(TData data, byte[] expectedValue) where TData : IObject
    {
        using var writer = new ArrayPoolBufferWriter<byte>();

        data.Serialize(writer);
        var a = writer.WrittenSpan.ToArray().ToHexString();
        writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_SubObject()
    {
        var expectedValue = "040000000200000001000000".ToBytes();
        var obj = new TestObjectWithSubObject { SubObject = new SubObject { Id = 1 } };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_SubObject2()
    {
        var expectedValue = "04000000030000000100000000000000".ToBytes();
        var obj = new TestObjectWithSubObject { SubObject = new SubObject2 { Id = 1L } };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_TVector_Of_TLObject_Interface()
    {
        var expectedValue = "0600000015C4B51C00000000".ToBytes();
        var obj = new TestObjectWithTVectorOfInterface { SubObjects = new TVector<ISubObject>() };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);
        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_RpcResult()
    {
        var expectedValue =
            "016D5CF363000000000000000600000015C4B51C020000000200000001000000230000000000000015C4B51C01000000240000000200000015C4B51C010000000431313131000000"
                .ToBytes();
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        //using var writer = new ArrayPoolBufferWriter<byte>();

        var obj = new TRpcResult
        {
            ReqMsgId = 99,
            Result = new TestObjectWithTVectorOfInterface
            {
                SubObjects = new TVector<ISubObject>(new SubObject { Id = 1 },
                    new SubObject3WithNullableProperty
                    {
                        Level2Vector =
                            new TVector<ILevel2SubObject>(new Level2SubObject
                            {
                                Id = 2,
                                Level2SubVector = new TVector<string>("1111")
                            }),
                        Level2Vector2 = new TVector<ILevel2SubObject>()// ==null or .Count==0 is the same result for TVector<T>?
                    })
            }
        };

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_Test()
    {
        var expectedValue = "01000000010000000974657374206E616D650000".ToBytes();
        var obj = new TestObject { TestId = 1, Name = "test name" };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_Empty_TLObject_Interface()
    {
        var expectedValue = "15C4B51C00000000".ToBytes();
        var obj = new TVector<ISubObject>();
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_SimpleType()
    {
        var expectedValue =
            "15C4B51C0500000001000000000000000200000000000000030000000000000004000000000000000500000000000000"
                .ToBytes();
        var obj = new TVector<long>(1,
            2,
            3,
            4,
            5);
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_TLObject()
    {
        var expectedValue = "15C4B51C010000000500000003000000B57572990A746573742076616C756500".ToBytes();
        var obj = new TVector<TestObjectWithNullableProperty>(
            new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" });
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_TLObject_Interface()
    {
        var expectedValue = "15C4B51C020000000200000001000000030000000200000000000000".ToBytes();
        var obj = new TVector<ISubObject>(new SubObject { Id = 1 }, new SubObject2 { Id = 2 });
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = new ArrayPoolBufferWriter<byte>();

        //obj.Serialize(writer);

        ////writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }
}

public class TestNoneIObject
{
}

[TlObject(01)]
public class TestObject : IObject
{
    public string Name { get; set; }
    public int TestId { get; set; }
    public uint ConstructorId => 0x01;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(TestId);
        writer.Write(Name);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //TestId = buffer.Deserialize<int>();
        //Name = buffer.Deserialize<string>();
        TestId = buffer.ReadInt32();
        Name = buffer.ReadString();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    bw.Write(TestId);
    //    //SerializerFactory.CreateSerializer<int>().Serialize(TestId, bw);
    //    SerializerFactory.CreateSerializer<string>().Serialize(Name, bw);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    TestId = br.ReadInt32();
    //    Name = SerializerFactory.CreateSerializer<string>().Deserialize(br);
    //}
}

[TlObject(0x6)]
public class TestObjectWithTVectorOfInterface : IObject
{
    public TVector<ISubObject> SubObjects { get; set; }
    public uint ConstructorId => 0x6;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(SubObjects);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //SubObjects = buffer.Deserialize<TVector<ISubObject>>();
        SubObjects = buffer.Read<TVector<ISubObject>>();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    SubObjects.Serialize(bw);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    SubObjects = SerializerFactory.CreateSerializer<TVector<ISubObject>>().Deserialize(br);
    //}
}

[TlObject(0x5)]
public class TestObjectWithNullableProperty : IObject
{
    public bool? BoolValue1 { get; set; }
    //public BitArray Flags { get; set; } = new(32);
    public int Flags { get; set; }
    public int? IntValue { get; set; }
    public string? StringValue { get; set; }
    public uint ConstructorId => 0x5;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);

        ComputeFlag();
        //SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, writer);
        writer.Write(Flags);
        if (BoolValue1.HasValue)
        {
            SerializerFactory.CreateSerializer<bool>().Serialize(BoolValue1.Value, writer);
        }

        if (StringValue != null)
        {
            SerializerFactory.CreateSerializer<string>().Serialize(StringValue, writer);
        }

        if (IntValue.HasValue)
        {
            //SerializerFactory.CreateSerializer<int>().Serialize(IntValue.Value, bw);
            writer.Write(IntValue.Value);
        }
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        Flags = buffer.ReadInt32();
        if (Flags.IsBitSet(0))
        {
            BoolValue1 = buffer.Read();
        }

        if (Flags.IsBitSet(1))
        {
            StringValue = buffer.ReadString();
        }

        if (Flags.IsBitSet(2))
        {
            //IntValue = SerializerFactory.CreateSerializer<int>().Deserialize(br);
            //IntValue = buffer.Deserialize<int>();
            IntValue = buffer.ReadInt32();
        }
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);

    //    ComputeFlag();
    //    SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, bw);
    //    if (BoolValue1.HasValue)
    //    {
    //        SerializerFactory.CreateSerializer<bool>().Serialize(BoolValue1.Value, bw);
    //    }

    //    if (StringValue != null)
    //    {
    //        SerializerFactory.CreateSerializer<string>().Serialize(StringValue, bw);
    //    }

    //    if (IntValue.HasValue)
    //    {
    //        //SerializerFactory.CreateSerializer<int>().Serialize(IntValue.Value, bw);
    //        bw.Write(IntValue.Value);
    //    }
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    Flags = SerializerFactory.CreateSerializer<BitArray>().Deserialize(br);
    //    if (Flags[0])
    //    {
    //        BoolValue1 = SerializerFactory.CreateSerializer<bool>().Deserialize(br);
    //    }

    //    if (Flags[1])
    //    {
    //        StringValue = SerializerFactory.CreateSerializer<string>().Deserialize(br);
    //    }

    //    if (Flags[2])
    //    {
    //        //IntValue = SerializerFactory.CreateSerializer<int>().Deserialize(br);
    //        IntValue = br.ReadInt32();
    //    }
    //}

    public void ComputeFlag()
    {
        if (BoolValue1 != null)
        {
            //Flags[0] = true;
            Flags = Flags.SetBit(0);
        }

        if (StringValue != null)
        {
            //Flags[1] = true;
            Flags = Flags.SetBit(1);
        }

        if (IntValue != null)
        {
            //Flags[2] = true;
            Flags = Flags.SetBit(2);
        }
    }
}

[TlObject(04)]
public class TestObjectWithSubObject : IObject
{
    public ISubObject SubObject { get; set; }
    public uint ConstructorId => 0x4;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(SubObject);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //SubObject = buffer.Deserialize<ISubObject>();
        SubObject = buffer.Read<ISubObject>();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    SubObject.Serialize(bw);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    SubObject = SerializerFactory.CreateSerializer<ISubObject>().Deserialize(br);
    //}
}

public interface ISubObject : IObject
{
}

[TlObject(02)]
public class SubObject : ISubObject
{
    public int Id { get; set; }
    public uint ConstructorId => 0x02;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(Id);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        Id = buffer.ReadInt32();
        //Id = buffer.Deserialize<int>();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    bw.Write(Id);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    Id = SerializerFactory.CreateSerializer<int>().Deserialize(br);
    //}
}

[TlObject(03)]
public class SubObject2 : ISubObject
{
    public long Id { get; set; }
    public uint ConstructorId => 0x03;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(Id);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //Id = buffer.Deserialize<long>();
        Id = buffer.ReadInt64();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    bw.Write(Id);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    Id = SerializerFactory.CreateSerializer<long>().Deserialize(br);
    //}
}

public interface ILevel2SubObject : IObject
{
}

[TlObject(0x24)]
public class Level2SubObject : ILevel2SubObject
{
    public int Id { get; set; }

    public TVector<string> Level2SubVector { get; set; }
    public uint ConstructorId => 0x24;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);
        writer.Write(Id);
        writer.Write(Level2SubVector);
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        //Id = buffer.Deserialize<int>();
        //Level2SubVector = buffer.Deserialize<TVector<string>>();
        Id = buffer.ReadInt32();
        Level2SubVector = buffer.Read<TVector<string>>();
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    bw.Write(ConstructorId);
    //    bw.Write(Id);
    //    Level2SubVector.Serialize(bw);
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    Id = br.ReadInt32();
    //    Level2SubVector = SerializerFactory.CreateSerializer<TVector<string>>().Deserialize(br);
    //}
}

[TlObject(0x23)]
public class SubObject3WithNullableProperty : ISubObject
{
    public int Flags { get; set; }
    public TVector<ILevel2SubObject> Level2Vector { get; set; }
    public TVector<ILevel2SubObject>? Level2Vector2 { get; set; }
    public uint ConstructorId => 0x23;
    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        //SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, writer);
        writer.Write(Flags);
        writer.Write(Level2Vector);
        if (Flags.IsBitSet(0))
        {
            Level2Vector?.Serialize(writer);
        }
    }

    public void Deserialize(ref ReadOnlyMemory<byte> buffer)
    {
        Flags = buffer.ReadInt32();
        Level2Vector = buffer.Read<TVector<ILevel2SubObject>>();
        if (Flags.IsBitSet(0))
        {
            Level2Vector2 = buffer.Read<TVector<ILevel2SubObject>>();
        }
    }

    public int GetLength()
    {
        throw new NotImplementedException();
    }

    //public void Serialize(BinaryWriter bw)
    //{
    //    ComputeFlag();
    //    bw.Write(ConstructorId);
    //    SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, bw);

    //    Level2Vector.Serialize(bw);

    //    if (Flags[0])
    //    {
    //        Level2Vector2?.Serialize(bw);
    //    }
    //}

    //public void Deserialize(BinaryReader br)
    //{
    //    Flags = SerializerFactory.CreateSerializer<BitArray>().Deserialize(br);
    //    Level2Vector = SerializerFactory.CreateSerializer<TVector<ILevel2SubObject>>().Deserialize(br);
    //    if (Flags[0])
    //    {
    //        Level2Vector2 = SerializerFactory.CreateSerializer<TVector<ILevel2SubObject>>().Deserialize(br);
    //    }
    //}

    public void ComputeFlag()
    {
        if (Level2Vector2?.Count > 0)
        {
            Flags = Flags.SetBit(0);
        }
    }
}