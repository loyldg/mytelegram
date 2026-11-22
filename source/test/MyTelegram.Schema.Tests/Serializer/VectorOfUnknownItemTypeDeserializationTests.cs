namespace MyTelegram.Schema.Serializer;

/// <summary>
/// Boxed vectors read as an interface (rpc_result.result, gzip_packed data, container message body...),
/// the item type is not known by the caller and has to be detected from the payload.
/// </summary>
public class VectorOfUnknownItemTypeDeserializationTests
{
    // rpc_result#f35c6d01 req_msg_id:7710980236957144064 result:Vector<int> [1001] (stories.readStories result)
    private const string RpcResultWithVectorOfInt32Hex = "016D5CF30054E3C5ABE7026B15C4B51C01000000E9030000";

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_Int32()
    {
        var bytes = RpcResultWithVectorOfInt32Hex.ToBytes();

        var rpcResult = Deserialize<IObject>(bytes).ShouldBeOfType<TRpcResult>();

        rpcResult.ReqMsgId.ShouldBe(7710980236957144064);
        var vector = rpcResult.Result.ShouldBeOfType<TVector<int>>();
        vector.ToArray().ShouldBe([1001]);
        rpcResult.ToBytes().ShouldBe(bytes);
    }

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_Int32_Multiple_Items()
    {
        var expected = new TRpcResult { ReqMsgId = 1, Result = new TVector<int>(1, 2, 3, -1) };

        AssertRoundTrip<TVector<int>>(expected);
    }

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_Int32_First_Item_Equals_Known_ConstructorId()
    {
        // 0x997275b5 is the constructor id of boolTrue
        var expected = new TRpcResult { ReqMsgId = 1, Result = new TVector<int>(unchecked((int)0x997275b5), 2, 3) };

        AssertRoundTrip<TVector<int>>(expected);
    }

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_Int64()
    {
        var expected = new TRpcResult { ReqMsgId = 2, Result = new TVector<long>(5_000_000_001, 5_000_000_002) };

        AssertRoundTrip<TVector<long>>(expected);
    }

    [Fact]
    public void Deserialize_RpcResult_With_Empty_Vector()
    {
        var expected = new TRpcResult { ReqMsgId = 3, Result = new TVector<int>() };

        var actual = AssertRoundTrip<TVector<int>>(expected);

        ((TVector<int>)actual.Result).Count.ShouldBe(0);
    }

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_Bool()
    {
        var expected = new TRpcResult { ReqMsgId = 4, Result = new TVector<IBool>(new TBoolTrue(), new TBoolFalse()) };

        var actual = AssertRoundTrip<TVector<IBool>>(expected);

        var vector = (TVector<IBool>)actual.Result;
        vector[0].ShouldBeOfType<TBoolTrue>();
        vector[1].ShouldBeOfType<TBoolFalse>();
    }

    [Fact]
    public void Deserialize_RpcResult_With_Vector_Of_TlObject()
    {
        var expected = new TRpcResult
        {
            ReqMsgId = 5,
            Result = new TVector<IPeer>(new TPeerUser { UserId = 1 }, new TPeerUser { UserId = 2 })
        };

        var actual = AssertRoundTrip<TVector<IPeer>>(expected);

        ((TVector<IPeer>)actual.Result).Cast<TPeerUser>().Select(p => p.UserId).ShouldBe([1L, 2L]);
    }

    [Fact]
    public void Deserialize_Vector_Of_Int32_As_IObject_Twice_Uses_Same_Result()
    {
        var bytes = new TVector<int>(7, 8).ToBytes();

        Deserialize<IObject>(bytes).ShouldBeOfType<TVector<int>>().ToArray().ShouldBe([7, 8]);
        Deserialize<IObject>(bytes).ShouldBeOfType<TVector<int>>().ToArray().ShouldBe([7, 8]);
    }

    [Fact]
    public void Deserialize_Vector_Of_Unsupported_Item_Type_As_IObject_Should_Throw()
    {
        // Vector<string> ["abc"], the item size does not match int/long and there is no item constructor id
        var bytes = new TVector<string>("abcdefghijk").ToBytes();

        Should.Throw<NotSupportedException>(() => Deserialize<IObject>(bytes));
    }

    [Fact]
    public void Deserialize_Concrete_Type_With_Unknown_ConstructorId_Should_Throw()
    {
        var bytes = "78563412010000000000000000".ToBytes();

        Should.Throw<NotSupportedException>(() => Deserialize<TPeerUser>(bytes));
    }

    [Fact]
    public void Deserialize_Typed_Vector_With_Wrong_ConstructorId_Should_Throw()
    {
        var bytes = "7856341200000000".ToBytes();

        Should.Throw<NotSupportedException>(() => Deserialize<TVector<int>>(bytes));
    }

    [Fact]
    public void Deserialize_MsgContainer_With_RpcResult_Of_Vector_Of_Int32_Not_Last()
    {
        var rpcResult = new TRpcResult { ReqMsgId = 10, Result = new TVector<int> { 1001 } };
        var ack = new TMsgsAck { MsgIds = new TVector<long>(11, 12) };
        var container = new TMsgContainer
        {
            Messages =
            [
                new TContainerMessage { MsgId = 1, SeqNo = 1, Bytes = rpcResult.ToBytes().Length, Body = rpcResult },
                new TContainerMessage { MsgId = 2, SeqNo = 2, Bytes = ack.ToBytes().Length, Body = ack }
            ]
        };
        var bytes = container.ToBytes();

        var actual = Deserialize<IObject>(bytes).ShouldBeOfType<TMsgContainer>();

        actual.Messages.Length.ShouldBe(2);
        actual.Messages[0].Body.ShouldBeOfType<TRpcResult>().Result.ShouldBeOfType<TVector<int>>().ToArray().ShouldBe([1001]);
        actual.Messages[1].Body.ShouldBeOfType<TMsgsAck>().MsgIds.ToArray().ShouldBe([11L, 12L]);
    }

    [Fact]
    public void Deserialize_MsgContainer_With_Invalid_Message_Length_Should_Throw()
    {
        var ack = new TMsgsAck { MsgIds = new TVector<long> { 11 } };
        var container = new TMsgContainer
        {
            Messages = [new TContainerMessage { MsgId = 1, SeqNo = 1, Bytes = 1000, Body = ack }]
        };

        Should.Throw<InvalidDataException>(() => Deserialize<IObject>(container.ToBytes()));
    }

    [Fact]
    public void GetLength_Of_Vector_Of_Bool()
    {
        var vector = new TVector<bool>(true, false, true);

        vector.GetLength().ShouldBe(vector.ToBytes().Length);
    }

    [Fact]
    public void CreateSerializer_Should_Return_Cached_Instance()
    {
        SerializerFactory.CreateSerializer<IObject>().ShouldBeSameAs(SerializerFactory.CreateSerializer<IObject>());
        SerializerFactory.CreateSerializer<TVector<int>>().ShouldBeSameAs(SerializerFactory.CreateSerializer<TVector<int>>());
        SerializerFactory.CreateVectorSerializer<long>().ShouldBeSameAs(SerializerFactory.CreateVectorSerializer<long>());
    }

    [Fact]
    public void CreateSerializer_Of_Unsupported_Type_Should_Throw()
    {
        Should.Throw<NotImplementedException>(() => SerializerFactory.CreateSerializer<DateTime>());
    }

    private static TRpcResult AssertRoundTrip<TExpectedResult>(TRpcResult expected)
    {
        var bytes = expected.ToBytes();

        var actual = Deserialize<IObject>(bytes).ShouldBeOfType<TRpcResult>();

        actual.ReqMsgId.ShouldBe(expected.ReqMsgId);
        actual.Result.ShouldBeOfType<TExpectedResult>();
        actual.ToBytes().ShouldBe(bytes);

        return actual;
    }

    private static T Deserialize<T>(byte[] bytes) where T : IObject
    {
        ReadOnlyMemory<byte> buffer = bytes;
        var obj = buffer.Read<T>();
        buffer.Length.ShouldBe(0);

        return obj;
    }
}
