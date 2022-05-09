namespace MyTelegram.Schema.Serializer;

public class BooleanSerializerTests
{
    [Fact]
    public void Deserialize_ErrorData_Throws_Argument_Exception()
    {
        var value = new byte[] { 1, 2, 3, 4 };
        var br = new BinaryReader(new MemoryStream(value));
        var serializer = CreateSerializer();
        Assert.Throws<ArgumentException>(() => serializer.Deserialize(br));
    }

    [InlineData(new byte[] { 55, 151, 121, 188 }, false)]
    [InlineData(new byte[] { 181, 117, 114, 153 }, true)]
    [Theory]
    public void DeserializeTest(byte[] value,
        bool expectedValue)
    {
        var stream = new MemoryStream(value);
        var br = new BinaryReader(stream);
        var serializer = CreateSerializer();

        var actualValue = serializer.Deserialize(br);

        actualValue.ShouldBe(expectedValue);
    }

    [InlineData(false, new byte[] { 55, 151, 121, 188 })]
    [InlineData(true, new byte[] { 181, 117, 114, 153 })]
    [Theory]
    public void SerializeTest(bool value,
        byte[] expectedValue)
    {
        var stream = new MemoryStream();
        var bw = new BinaryWriter(stream);
        var serializer = CreateSerializer();

        serializer.Serialize(value, bw);

        stream.ToArray().ShouldBeEquivalentTo(expectedValue);
    }

    private BooleanSerializer CreateSerializer() => new();
}
