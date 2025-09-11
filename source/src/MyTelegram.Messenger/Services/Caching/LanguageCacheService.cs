using System.Collections.Frozen;

namespace MyTelegram.Messenger.Services.Caching;

public class LanguageTextItem(string key, int languageVersion)
{
    public string Key { get; init; } = key;
    public string? Value { get; set; } = null;
    public string? ZeroValue { get; set; } = null;
    public string? OneValue { get; set; } = null;
    public string? TwoValue { get; set; } = null;
    public string? FewValue { get; set; } = null;
    public string? ManyValue { get; set; } = null;
    public string? OtherValue { get; set; } = null;
    public int LanguageVersion { get; } = languageVersion;
}

public interface ILanguageCacheService
{
    Task LoadAllLanguagesAsync();
    Task LoadAllLanguageTextAsync();
    Task<IReadOnlyCollection<ILanguageReadModel>> GetAllLanguagesAsync(string languagePack);

    Task<IReadOnlyCollection<LanguageTextItem>> GetLanguageTextAsync(string languageCode,
        string languagePack);

    Task UpdateLanguageTextAsync(string languageCode, string languagePack, string key, string value);
    Task DeleteLanguageTextAsync(string languageCode, string languagePack, string key);

    Task<List<LanguageTextItem>> GetLanguageTextsAsync(string languageCode, string languagePack, IEnumerable<string> keys);

    Task<ILanguageReadModel?> GetLanguageAsync(string languageCode, string languagePack);

    Task<List<LanguageTextItem>> GetLanguageDifferenceAsync(string languageCode, string languagePack, int fromVersion);
}

public class LanguageCacheService(IQueryProcessor queryProcessor, ILogger<LanguageCacheService> logger) : ILanguageCacheService, ISingletonDependency
{
    private FrozenDictionary<string, FrozenDictionary<string, ILanguageReadModel>> _languageReadModels = new Dictionary<string, FrozenDictionary<string, ILanguageReadModel>>().ToFrozenDictionary();
    private FrozenDictionary<string, Dictionary<string, LanguageTextItem>> _languageTexts = new Dictionary<string, Dictionary<string, LanguageTextItem>>().ToFrozenDictionary();

    public async Task LoadAllLanguagesAsync()
    {
        var languages = await queryProcessor.ProcessAsync(new GetAllLanguagesQuery());
        var groupedLanguages = languages.GroupBy(p => p.Platform)
            .ToDictionary(k => GetLanguagePack(k.Key),
                v => v.ToFrozenDictionary(x => GetLanguageTextKey(x.LanguageCode, x.Platform, false)));
        _languageReadModels = groupedLanguages.ToFrozenDictionary();
        logger.LogInformation("Loading all languages completed, count: {Count}", languages.Count);
    }

    public async Task LoadAllLanguageTextAsync()
    {
        var sw = Stopwatch.StartNew();
        var languageTexts = await queryProcessor.ProcessAsync(new GetAllLanguageTextsQuery());
        _languageTexts = languageTexts.GroupBy(p => new { p.LanguageCode, p.Platform },
                v => new LanguageTextItem(v.Key, v.LanguageVersion) {
                    Value = v.Value,
                    ZeroValue = v.ZeroValue,
                    OneValue = v.OneValue,
                    TwoValue = v.TwoValue,
                    FewValue = v.FewValue,
                    ManyValue = v.ManyValue,
                    OtherValue = v.OtherValue
                })
            .ToFrozenDictionary(k => GetLanguageTextKey(k.Key.LanguageCode, k.Key.Platform, false),
                v => v.ToDictionary(k1 => k1.Key, v1 => v1));
        sw.Stop();
        logger.LogInformation("Loading all language texts completed, count: {Count}, time: {TimeSpan}", languageTexts.Count, sw.Elapsed);
    }

    private string GetLanguageTextKey(string languageCode, string languagePack, bool removeSuffix = true)
    {
        if (removeSuffix)
        {
            return $"{GetLanguageCode(languageCode)}_{languagePack}";
        }

        return $"{languageCode}_{languagePack}";
    }

    private string GetLanguagePack(DeviceType deviceType)
    {
        var langPack = deviceType.ToString().ToLower();
        switch (deviceType)
        {
            case DeviceType.Desktop:
                langPack = "tdesktop";
                break;
            case DeviceType.AndroidX:
                langPack = "android_x";
                break;
        }

        return langPack;
    }
    private string GetLanguageTextKey(string languageCode, DeviceType deviceType, bool removeSuffix = true)
    {
        var langPack = GetLanguagePack(deviceType);

        return GetLanguageTextKey(languageCode, langPack, removeSuffix);
    }

    public async Task<IReadOnlyCollection<ILanguageReadModel>> GetAllLanguagesAsync(string languagePack)
    {
        if (_languageReadModels.Count == 0)
        {
            await LoadAllLanguagesAsync();
        }

        if (_languageReadModels.TryGetValue(languagePack, out var languages))
        {
            return languages.Values;
        }

        return [];
    }

    public async Task<IReadOnlyCollection<LanguageTextItem>> GetLanguageTextAsync(string languageCode,
         string languagePack)
    {
        if (_languageTexts.Count == 0)
        {
            await LoadAllLanguageTextAsync();
        }
        var key = GetLanguageTextKey(languageCode, languagePack);

        if (_languageTexts.TryGetValue(key, out var languageTextItems))
        {
            return languageTextItems.Values;
        }

        return [];
    }

    public Task UpdateLanguageTextAsync(string languageCode, string languagePack, string key, string value)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            if (texts.TryGetValue(key, out var item))
            {
                item.Value = value;
                logger.LogInformation("Language text updated, languageCode: {Code}, platform: {Platform}, key: {Key}", languageCode, languagePack, key);
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteLanguageTextAsync(string languageCode, string languagePack, string key)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            texts.Remove(key);
            logger.LogInformation("Language text deleted, languageCode: {Code}, platform: {Platform}, key: {Key}", languageCode, languagePack, key);
        }

        return Task.CompletedTask;
    }

    public Task<List<LanguageTextItem>> GetLanguageTextsAsync(string languageCode, string languagePack, IEnumerable<string> keys)
    {
        var languageTexts = new List<LanguageTextItem>();

        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            foreach (var key in keys)
            {
                if (texts.TryGetValue(key, out var item))
                {
                    languageTexts.Add(item);
                }
            }
        }

        return Task.FromResult(languageTexts);
    }

    public Task<ILanguageReadModel?> GetLanguageAsync(string languageCode, string languagePack)
    {
        ILanguageReadModel? languageReadModel = null;
        if (_languageReadModels.TryGetValue(languagePack, out var languages))
        {
            var key = GetLanguageTextKey(languageCode, languagePack);
            languages.TryGetValue(key, out languageReadModel);
        }

        return Task.FromResult(languageReadModel);
    }

    public Task<List<LanguageTextItem>> GetLanguageDifferenceAsync(string languageCode, string languagePack, int fromVersion)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            return Task.FromResult(texts.Values.Where(p => p.LanguageVersion > fromVersion).ToList());
        }

        return Task.FromResult<List<LanguageTextItem>>([]);
    }

    private string GetLanguageCode(string langCode)
    {
        // The WebA client uses language codes with the `-raw` suffix
        if (langCode.EndsWith("-raw"))
        {
            return langCode[..^4];
        }

        return langCode;
    }
}
