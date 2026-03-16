namespace MyTelegram.DataSeeder;

public class DataSeederService(
    ILogger<DataSeederService> logger,
    IDataSeederHelper dataSeederHelper,
	IEnumerable<IDataSeeder> dataSeeders
) : IDataSeederService, ITransientDependency
{
    public async Task SeedAllAsync()
    {
        try
        {
			foreach (var dataSeeder in dataSeeders)
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