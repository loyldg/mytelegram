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
        var writer = ArrayBufferWriterPool.Rent();
        var serializer = CreateSerializer();

        serializer.Serialize(value, writer.Writer);

        writer.Writer.WrittenSpan.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    [MemberData(nameof(GetData), parameters: new[] { 0 })]
    [MemberData(nameof(GetData), parameters: new[] { 1 })]
    [MemberData(nameof(GetData), parameters: new[] { 2 })]
    [MemberData(nameof(GetData), parameters: new[] { 31 })]
    [MemberData(nameof(GetData), parameters: new[] { 1, 2 })]
    [MemberData(nameof(GetData), parameters: new[] { 1, 2, 3 })]
    [MemberData(nameof(GetData), parameters: new[] { 1, 2, 31 })]
    [Theory]
    public void DeserializeTest(byte[] value, BitArray expectedValue)
    {
        //var stream = new MemoryStream(value);
        //var br = new BinaryReader(stream);
        var serializer = CreateSerializer();
        var reader = new SequenceReader<byte>(new ReadOnlySequence<byte>(value));
        var actualValue = serializer.Deserialize(ref reader);

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