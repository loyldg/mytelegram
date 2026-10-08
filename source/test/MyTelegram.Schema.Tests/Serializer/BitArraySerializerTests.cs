using System.Buffers;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Schema.Serializer;

public class BitArraySerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var value = new BitArray(32) { [0] = true };
        var expectedValue = "01000000".ToBytes();
        //var stream = new MemoryStream();
        //var bw = new BinaryWriter(stream);
        using var writer = new ArrayPoolBufferWriter<byte>();
        var serializer = CreateSerializer();

        serializer.Serialize(value, writer);

        writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [MemberData(nameof(GetData), arguments: new[] { 0 })]
    [MemberData(nameof(GetData), arguments: new[] { 1 })]
    [MemberData(nameof(GetData), arguments: new[] { 2 })]
    [MemberData(nameof(GetData), arguments: new[] { 31 })]
    [MemberData(nameof(GetData), arguments: new[] { 1, 2 })]
    [MemberData(nameof(GetData), arguments: new[] { 1, 2, 3 })]
    [MemberData(nameof(GetData), arguments: new[] { 1, 2, 31 })]
    [Theory]
    public void DeserializeTest(byte[] value, BitArray expectedValue)
    {
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        var serializer = CreateSerializer();
        ReadOnlyMemory<byte> buffer = value;
        var actualValue = serializer.Deserialize(ref buffer);

        actualValue.ShouldBeEquivalentTo(expectedValue);
    }

    public static IEnumerable<object[]> GetData(params int[] bitOfNSetToTrue)
    {
        var bitArray = new BitArray(32);
        foreach (var i in bitOfNSetToTrue)
        {
            bitArray.Set(i, true);
        }

        var buffer = new byte[4];
        bitArray.CopyTo(buffer, 0);

        var allData = new List<object[]> { new object[] { buffer, bitArray } };

        return allData;
    }

    private BitArraySerializer CreateSerializer() => new();
}