namespace MyTelegram.DataSeeder.DataSeeders;

public interface IDataSeeder<out TData>
{
    Task SeedAsync(string jsonFileName, Func<TData, Task> seedAction);
    Task Seed2Async(string jsonFileName, Func<TData, Task> seedAction);
}

public interface IDataSeeder
{
    Task SeedAsync();
}