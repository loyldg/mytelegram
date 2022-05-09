namespace MyTelegram.Schema.Serializer;

public class Int128SerializerTests
{
    [Fact]
    public void SerializeTest()
    {
        var value = "C0CC11F66E1111B8529BB89742D77959".ToBytes();
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        var serializer = CreateSerializer();

        serializer.Serialize(value, bw);

        stream.ToArray().ShouldBeEquivalentTo(value);
    }

    [Fact]
    public void DeserializeTest()
    {
        var expectedBytes = "C0CC11F66E1111B8529BB89742D77959".ToBytes();
        var stream = new MemoryStream(expectedBytes);
        var br = new BinaryReader(stream);
        var serializer = CreateSerializer();

        var actualBytes = serializer.Deserialize(br);

        actualBytes.ShouldBeEquivalentTo(expectedBytes);
    }

    private Int128Serializer CreateSerializer() => new();
}