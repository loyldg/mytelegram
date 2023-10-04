using System.Reflection;
using MyTelegram.Schema.Extensions;
using MyTelegram.Schema.Messages;
using MyTelegram.Schema.Upload;
using MyTelegram.Schema.Users;

#pragma warning disable CS8618

namespace MyTelegram.Schema.Serializer;

public class ObjectSerializerTests
{
    public ObjectSerializerTests()
    {
        // Only need this code when tl object and serializer in different assembly
        SerializerObjectMappings.CreateConstructIdToTypeMappingsFromAssembly(Assembly.GetExecutingAssembly());
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
        
        SerializeTest(obj,expectedValue);
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
        //using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);
        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);

        //obj.Serialize(bw);

        var bytes = obj.ToBytes();

        bytes.ShouldBeEquivalentTo(expectedValue);
        //writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        obj.ComputeFlag();
        //var stream = new MemoryStream();
        //var br = new BinaryReader(stream);
        ////var buffer = new ReadOnlySequence<byte>(value);

        //var actualObj = br.Deserialize<RequestGetFile>();
        var actualObj = value.ToTObject<RequestGetFile>();

        actualObj.ShouldBeEquivalentTo(obj);
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
    //    //using var writer = ArrayBufferWriterPool.Rent();

    //    //serializer.Serialize(obj, writer.Writer);
    //    //var a = writer.Writer.WrittenSpan.ToArray().ToHexString();

    //    //writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);

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
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));
        var obj = serializer.Deserialize(ref reader);

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
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value.AsSpan().Slice(20).ToArray()));

        var obj = serializer.Deserialize(ref reader);

        reader.Consumed.ShouldBe(value.Length-12-20);
    }

    //public void D
    // 0x48a5910d15c4b51c010000004ca5e8dd81841e00000000000
    //48a5910d15c4b51c010000004ca5e8dd81841e0000000000b0bbd1de6b2f5bda

    [Fact]
    public void Deserialize_TVector_Of_IUpdates()
    {
        var bytes = Convert.FromBase64String(
            "FcS1HAEAAADZBLpi4G4ROAABAAARJwAAIhdRWYGEHgAAAAAAHjelogNAt0O6AAAAXNDIZAV3YWZhZgAAEScAAAEAAAA=");
        var obj = bytes.ToTObject<TVector<IUpdate>>();

        obj.Count.ShouldBe(1);
    }

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
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));

        var actualObj = serializer.Deserialize(ref reader);

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
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));

        var actualObj = serializer.Deserialize(ref reader);

        actualObj.ShouldBeEquivalentTo(expectedValue);
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
        using var writer = ArrayBufferWriterPool.Rent();

        Assert.Throws<NotSupportedException>(() =>
            serializer.Serialize(new TestNoneIObject(), writer.Writer));
    }

    [Fact]
    public void Serialize_Object_Contains_Flag_Property()
    {
        //                        "050000000403000000000000B57572990A746573742076616C756500"
        var expectedValue = "0500000003000000B57572990A746573742076616C756500".ToBytes();

        var obj = new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
        //stream.ToArray().ShouldBeEquivalentTo(expectedValue);

        SerializeTest(obj, expectedValue);
    }

    private void SerializeTest<TData>(TData data, byte[] expectedValue) where TData : IObject
    {
        using var writer = ArrayBufferWriterPool.Rent();

        data.Serialize(writer.Writer);
        var a = writer.Writer.WrittenSpan.ToArray().ToHexString();
        writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_SubObject()
    {
        var expectedValue = "040000000200000001000000".ToBytes();
        var obj = new TestObjectWithSubObject { SubObject = new SubObject { Id = 1 } };
        //var stream = new MemoryStream();
        //var writer = new BinaryWriter(stream);
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        //using var writer = ArrayBufferWriterPool.Rent();

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

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        ////using var writer = ArrayBufferWriterPool.Rent();

        //obj.Serialize(writer);

        ////writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //TestId = buffer.Deserialize<int>();
        //Name = buffer.Deserialize<string>();
        TestId = reader.ReadInt32();
        Name = reader.ReadString();
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //SubObjects = buffer.Deserialize<TVector<ISubObject>>();
        SubObjects = reader.Read<TVector<ISubObject>>();
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
    public BitArray Flags { get; set; } = new(32);
    public int? IntValue { get; set; }
    public string? StringValue { get; set; }
    public uint ConstructorId => 0x5;
    public void Serialize(IBufferWriter<byte> writer)
    {
        writer.Write(ConstructorId);

        ComputeFlag();
        SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, writer);
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0])
        {
            BoolValue1 = reader.Read();
        }

        if (Flags[1])
        {
            StringValue = reader.ReadString();
        }

        if (Flags[2])
        {
            //IntValue = SerializerFactory.CreateSerializer<int>().Deserialize(br);
            //IntValue = buffer.Deserialize<int>();
            IntValue = reader.ReadInt32();
        }
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
            Flags[0] = true;
        }

        if (StringValue != null)
        {
            Flags[1] = true;
        }

        if (IntValue != null)
        {
            Flags[2] = true;
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //SubObject = buffer.Deserialize<ISubObject>();
        SubObject = reader.Read<ISubObject>();
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt32();
        //Id = buffer.Deserialize<int>();
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //Id = buffer.Deserialize<long>();
        Id = reader.ReadInt64();
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

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        //Id = buffer.Deserialize<int>();
        //Level2SubVector = buffer.Deserialize<TVector<string>>();
        Id = reader.ReadInt32();
        Level2SubVector = reader.Read<TVector<string>>();
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
    public BitArray Flags { get; set; } = new(32);
    public TVector<ILevel2SubObject> Level2Vector { get; set; }
    public TVector<ILevel2SubObject>? Level2Vector2 { get; set; }
    public uint ConstructorId => 0x23;
    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, writer);
        writer.Write(Level2Vector);
        if (Flags[0])
        {
            Level2Vector?.Serialize(writer);
        }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Level2Vector = reader.Read<TVector<ILevel2SubObject>>();
        if (Flags[0])
        {
            Level2Vector2 = reader.Read<TVector<ILevel2SubObject>>();
        }
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
            Flags[0] = true;
        }
    }
}