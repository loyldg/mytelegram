using System.Reflection;

namespace MyTelegram.DataSeeder;

public class DataSeederService(
    ILogger<DataSeederService> logger,
    IDataSeederHelper dataSeederHelper,
    IEnumerable<IDataSeeder> dataSeeders) : IDataSeederService, ITransientDependency
{
    public async Task SeedAllAsync()
    {
        try
        {
            await dataSeederHelper.LoadDataSeederConfigAsync();

            var sortedDataSeeders = dataSeeders
                .OrderByDescending(x =>
                    x.GetType()
                        .GetCustomAttribute<DataSeederAttribute>()?.Order ?? 0)
                .ToList();

            foreach (var dataSeeder in sortedDataSeeders)
            {
                await dataSeeder.SeedAsync();
            }
        }
        finally
        {
            await dataSeederHelper.SaveDataSeederConfigAsync();
        }

        logger.LogInformation("All data created");
    }
}