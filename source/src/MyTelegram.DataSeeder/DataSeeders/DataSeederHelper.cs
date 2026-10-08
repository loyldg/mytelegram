using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MyTelegram.DataSeeder.DataSeeders;

public class DataSeederHelper(ILogger<DataSeederHelper> logger) : IDataSeederHelper, ISingletonDependency
{
    private DataSeederConfig? _config;

    public async Task SaveDataSeederConfigAsync()
    {
        var config = await LoadDataSeederConfigAsync();
        var json = JsonSerializer.Serialize(config);
        var fullName = Path.Combine(AppContext.BaseDirectory, DataSeederConsts.RootFolder,
            DataSeederConsts.DataSeederFileName);
        var dir = Path.GetDirectoryName(fullName);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        await File.WriteAllTextAsync(fullName, json);
    }

    public Task ResetDataSeederConfigAsync()
    {
        var fullName = Path.Combine(AppContext.BaseDirectory, DataSeederConsts.RootFolder,
            DataSeederConsts.DataSeederFileName);
        if (File.Exists(fullName))
        {
            File.Delete(fullName);
        }

        return Task.CompletedTask;
    }

    public async Task<string?> GetJsonTextAsync(string jsonFileName)
    {
        var fullName = jsonFileName;
        if (!Path.IsPathRooted(jsonFileName))
        {
            fullName = Path.Combine(AppContext.BaseDirectory, DataSeederConsts.RootFolder, jsonFileName);
        }

        if (!File.Exists(fullName))
        {
            logger.LogWarning("{FileName} not exists", fullName);
            return null;
        }

        return await File.ReadAllTextAsync(fullName);
    }

    public async Task<T?> ReadDataFromFileAsync<T>(string jsonFileName)
    {
        var jsonText = await GetJsonTextAsync(jsonFileName);
        if (string.IsNullOrEmpty(jsonText))
        {
            logger.LogWarning("Read data from json file failed, fileName: {JsonFile}", jsonFileName);
            return default;
        }

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };
        serializerOptions.Converters.Add(new BitArrayConverter());

        return JsonSerializer.Deserialize<T>(jsonText, serializerOptions);
    }

    public async Task<DataSeederConfig> LoadDataSeederConfigAsync()
    {
        return _config ??= await ReadDataFromFileAsync<DataSeederConfig>(DataSeederConsts.DataSeederFileName) ??
                           new DataSeederConfig();
    }
}