namespace MyTelegram.DataSeeder.DataSeeders;

public class DataSeeder<TData>(
    ILogger<DataSeeder<TData>> logger,
    IDataSeederHelper dataSeederHelper) : IDataSeeder<TData>
{
    public async Task SeedAsync(string jsonFileName, Func<TData, Task> seedAction)
    {
        var datas = await dataSeederHelper.ReadDataFromFileAsync<List<TData>>(jsonFileName);
        if (datas?.Count > 0)
        {
            foreach (var data in datas)
            {
                await seedAction(data);
            }

            logger.LogInformation("{TypeName} created successfully, count: {Count}", typeof(TData).Name, datas.Count);
        }
    }

    public async Task Seed2Async(string jsonFileName, Func<TData, Task> seedAction)
    {
        var datas = await dataSeederHelper.ReadDataFromFileAsync<TData>(jsonFileName);
        if (datas == null)
        {
            logger.LogWarning("No data found in file: {FileName}", jsonFileName);
            return;
        }
        await seedAction(datas);
    }
    public async Task SeedAsync()
    {
        // Do nothing
    }
}