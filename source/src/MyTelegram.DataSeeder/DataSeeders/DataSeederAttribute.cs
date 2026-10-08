namespace MyTelegram.DataSeeder.DataSeeders;

[AttributeUsage(AttributeTargets.Class)]
public class DataSeederAttribute(int order = 0) : Attribute
{
    public int Order { get; } = order;
}