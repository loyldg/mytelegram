namespace MyTelegram.Schema;

[AttributeUsage(AttributeTargets.Class)]
public class TlObjectAttribute : Attribute
{
    public uint ConstructorId { get; }

    public TlObjectAttribute(uint constructorId)
    {
        ConstructorId = constructorId;
    }
}