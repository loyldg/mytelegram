using System.Reflection;

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
    public void Deserialize_Object_Contains_Nullable_Property()
    {
        var value = "0500000003000000B57572990A746573742076616C756500".ToBytes();
        var expectedValue =
            new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value", IntValue = null };
        expectedValue.ComputeFlag();
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TestObjectWithNullableProperty>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_SubObject()
    {
        var value = "040000000200000001000000".ToBytes();
        var expectedValue = new TestObjectWithSubObject { SubObject = new SubObject { Id = 1 } };
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TestObjectWithSubObject>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_SubObject2()
    {
        var value = "04000000030000000100000000000000".ToBytes();
        var expectedValue = new TestObjectWithSubObject { SubObject = new SubObject2 { Id = 1L } };
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TestObjectWithSubObject>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_Object_Contains_TVector_Of_TLObject_Interface()
    {
        var value = @"0600000015C4B51C00000000".ToBytes();
        var expectedValue = new TestObjectWithTVectorOfInterface { SubObjects = new TVector<ISubObject>() };
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<IObject>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_RpcResult()
    {
        var value =
            "016D5CF363000000000000000600000015C4B51C020000000200000001000000230000000000000015C4B51C01000000240000000200000015C4B51C010000000431313131000000"
                .ToBytes();
        var expectedValue = new TRpcResult {
            ReqMsgId = 99,
            Result = new TestObjectWithTVectorOfInterface {
                SubObjects = new TVector<ISubObject>(new SubObject { Id = 1 },
                    new SubObject3WithNullableProperty {
                        Level2Vector =
                            new TVector<ILevel2SubObject>(new Level2SubObject {
                                Id = 2, Level2SubVector = new TVector<string>("1111")
                            }),
                        Level2Vector2 = null // new TVector<ILevel2SubObject>()
                    })
            }
        };
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TRpcResult>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_Test()
    {
        var value = "01000000010000000974657374206E616D650000".ToBytes();
        var expectedValue = new TestObject { TestId = 1, Name = "test name" };
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TestObject>();

        var actualValue = serializer.Deserialize(br);

        actualValue.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_Empty_TLObject_Interface()
    {
        var value = "15C4B51C00000000".ToBytes();
        var expectedValue = new TVector<ISubObject>();
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TVector<ISubObject>>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
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
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TVector<long>>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_TLObject()
    {
        var value = "15C4B51C010000000500000003000000B57572990A746573742076616C756500".ToBytes();
        var obj = new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" };
        obj.ComputeFlag();
        var expectedValue = new TVector<TestObjectWithNullableProperty>(obj);
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TVector<TestObjectWithNullableProperty>>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Deserialize_TVector_Of_TLObject_Interface()
    {
        var value = "15C4B51C020000000200000001000000030000000200000000000000".ToBytes();
        var expectedValue = new TVector<ISubObject>(new SubObject { Id = 1 }, new SubObject2 { Id = 2 });
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = new ObjectSerializer<TVector<ISubObject>>();

        var actualObject = serializer.Deserialize(br);

        actualObject.ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Non_IObject_Throws_Exception()
    {
        var serializer = new ObjectSerializer<TestNoneIObject>();

        Assert.Throws<NotSupportedException>(() =>
            serializer.Serialize(new TestNoneIObject(), new BinaryWriter(new MemoryStream())));
    }

    [Fact]
    public void Serialize_Object_Contains_Flag_Property()
    {
        var expectedValue = "0500000003000000B57572990A746573742076616C756500".ToBytes();
        var obj = new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_SubObject()
    {
        var expectedValue = "040000000200000001000000".ToBytes();
        var obj = new TestObjectWithSubObject { SubObject = new SubObject { Id = 1 } };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_SubObject2()
    {
        var expectedValue = "04000000030000000100000000000000".ToBytes();
        var obj = new TestObjectWithSubObject { SubObject = new SubObject2 { Id = 1L } };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Object_Contains_TVector_Of_TLObject_Interface()
    {
        var expectedValue = "0600000015C4B51C00000000".ToBytes();
        var obj = new TestObjectWithTVectorOfInterface { SubObjects = new TVector<ISubObject>() };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_RpcResult()
    {
        var expectedValue =
            "016D5CF363000000000000000600000015C4B51C020000000200000001000000230000000000000015C4B51C01000000240000000200000015C4B51C010000000431313131000000"
                .ToBytes();
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        var obj = new TRpcResult {
            ReqMsgId = 99,
            Result = new TestObjectWithTVectorOfInterface {
                SubObjects = new TVector<ISubObject>(new SubObject { Id = 1 },
                    new SubObject3WithNullableProperty {
                        Level2Vector =
                            new TVector<ILevel2SubObject>(new Level2SubObject {
                                Id = 2, Level2SubVector = new TVector<string>("1111")
                            }),
                        Level2Vector2 = new TVector<ILevel2SubObject>()// ==null or .Count==0 is the same result for TVector<T>?
                    })
            }
        };

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_Test()
    {
        var expectedValue = "01000000010000000974657374206E616D650000".ToBytes();
        var testObject = new TestObject { TestId = 1, Name = "test name" };
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        testObject.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_Empty_TLObject_Interface()
    {
        var expectedValue = "15C4B51C00000000".ToBytes();
        var obj = new TVector<ISubObject>();
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
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
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_TLObject()
    {
        var expectedValue = "15C4B51C010000000500000003000000B57572990A746573742076616C756500".ToBytes();
        var obj = new TVector<TestObjectWithNullableProperty>(
            new TestObjectWithNullableProperty { BoolValue1 = true, StringValue = "test value" });
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [Fact]
    public void Serialize_TVector_Of_TLObject_Interface()
    {
        var expectedValue = "15C4B51C020000000200000001000000030000000200000000000000".ToBytes();
        var obj = new TVector<ISubObject>(new SubObject { Id = 1 }, new SubObject2 { Id = 2 });
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);

        obj.Serialize(bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
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

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        bw.Write(TestId);
        //SerializerFactory.CreateSerializer<int>().Serialize(TestId, bw);
        SerializerFactory.CreateSerializer<string>().Serialize(Name, bw);
    }

    public void Deserialize(BinaryReader br)
    {
        TestId = br.ReadInt32();
        Name = SerializerFactory.CreateSerializer<string>().Deserialize(br);
    }
}

[TlObject(0x6)]
public class TestObjectWithTVectorOfInterface : IObject
{
    public TVector<ISubObject> SubObjects { get; set; }
    public uint ConstructorId => 0x6;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        SubObjects.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        SubObjects = SerializerFactory.CreateSerializer<TVector<ISubObject>>().Deserialize(br);
    }
}

[TlObject(0x5)]
public class TestObjectWithNullableProperty : IObject
{
    public bool? BoolValue1 { get; set; }
    public BitArray Flags { get; set; } = new(32);
    public int? IntValue { get; set; }
    public string? StringValue { get; set; }
    public uint ConstructorId => 0x5;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);

        ComputeFlag();
        SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, bw);
        if (BoolValue1.HasValue)
        {
            SerializerFactory.CreateSerializer<bool>().Serialize(BoolValue1.Value, bw);
        }

        if (StringValue != null)
        {
            SerializerFactory.CreateSerializer<string>().Serialize(StringValue, bw);
        }

        if (IntValue.HasValue)
        {
            //SerializerFactory.CreateSerializer<int>().Serialize(IntValue.Value, bw);
            bw.Write(IntValue.Value);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Flags = SerializerFactory.CreateSerializer<BitArray>().Deserialize(br);
        if (Flags[0])
        {
            BoolValue1 = SerializerFactory.CreateSerializer<bool>().Deserialize(br);
        }

        if (Flags[1])
        {
            StringValue = SerializerFactory.CreateSerializer<string>().Deserialize(br);
        }

        if (Flags[2])
        {
            //IntValue = SerializerFactory.CreateSerializer<int>().Deserialize(br);
            IntValue = br.ReadInt32();
        }
    }

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

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        SubObject.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        SubObject = SerializerFactory.CreateSerializer<ISubObject>().Deserialize(br);
    }
}

public interface ISubObject : IObject
{
}

[TlObject(02)]
public class SubObject : ISubObject
{
    public int Id { get; set; }
    public uint ConstructorId => 0x02;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        bw.Write(Id);
    }

    public void Deserialize(BinaryReader br)
    {
        Id = SerializerFactory.CreateSerializer<int>().Deserialize(br);
    }
}

[TlObject(03)]
public class SubObject2 : ISubObject
{
    public long Id { get; set; }
    public uint ConstructorId => 0x03;

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        bw.Write(Id);
    }

    public void Deserialize(BinaryReader br)
    {
        Id = SerializerFactory.CreateSerializer<long>().Deserialize(br);
    }
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

    public void Serialize(BinaryWriter bw)
    {
        bw.Write(ConstructorId);
        bw.Write(Id);
        Level2SubVector.Serialize(bw);
    }

    public void Deserialize(BinaryReader br)
    {
        Id = br.ReadInt32();
        Level2SubVector = SerializerFactory.CreateSerializer<TVector<string>>().Deserialize(br);
    }
}

[TlObject(0x23)]
public class SubObject3WithNullableProperty : ISubObject
{
    public BitArray Flags { get; set; } = new(32);
    public TVector<ILevel2SubObject> Level2Vector { get; set; }
    public TVector<ILevel2SubObject>? Level2Vector2 { get; set; }
    public uint ConstructorId => 0x23;

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        SerializerFactory.CreateSerializer<BitArray>().Serialize(Flags, bw);

        Level2Vector.Serialize(bw);

        if (Flags[0])
        {
            Level2Vector2?.Serialize(bw);
        }
    }

    public void Deserialize(BinaryReader br)
    {
        Flags = SerializerFactory.CreateSerializer<BitArray>().Deserialize(br);
        Level2Vector = SerializerFactory.CreateSerializer<TVector<ILevel2SubObject>>().Deserialize(br);
        if (Flags[0])
        {
            Level2Vector2 = SerializerFactory.CreateSerializer<TVector<ILevel2SubObject>>().Deserialize(br);
        }
    }

    public void ComputeFlag()
    {
        if (Level2Vector2?.Count > 0)
        {
            Flags[0] = true;
        }
    }
}
