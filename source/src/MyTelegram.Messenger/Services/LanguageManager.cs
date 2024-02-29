using System.Xml;

namespace MyTelegram.Messenger.Services;

public class LanguageManager : ILanguageManager
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, List<LangItem>>> _languages = new();


    private readonly ConcurrentDictionary<string, Dictionary<string, string>> _languageCodeToFileNames = new();
    public LanguageManager()
    {
        LoadDefaultLanguageFiles();
    }

    private void LoadDefaultLanguageFiles()
    {
        var languageFilePath = Path.Combine(AppContext.BaseDirectory, "Langs");
        if (!Directory.Exists(languageFilePath))
        {
            return;
        }

        var files = Directory.GetFiles(languageFilePath, "*");
        foreach (var file in files)
        {
            var name = Path.GetFileName(file);
            var items = name.Split("_");
            // Language fileName format:tdesktop_en_v2931836.strings
            if (items.Length == 3)
            {
                var languageCode = items[1];
                if (!_languageCodeToFileNames.TryGetValue(languageCode, out var deviceTypeToLanguageFiles))
                {
                    deviceTypeToLanguageFiles = new Dictionary<string, string>();
                    _languageCodeToFileNames.TryAdd(languageCode, deviceTypeToLanguageFiles);
                }

                var deviceType = items[0];
                if (!deviceTypeToLanguageFiles.ContainsKey(deviceType))
                {
                    deviceTypeToLanguageFiles.TryAdd(deviceType, file);
                }
            }
        }
    }

    public string GetDefaultLangPack(IRequestInput requestInput)
    {
        return requestInput.DeviceType.ToString().ToLower();
    }

    public async Task<List<LangItem>> GetAllLangPacksAsync(string langCode, string langPack)
    {
        var langFileName = string.Empty;

        if (_languages.TryGetValue(langCode, out var deviceTypeToLanguageFiles))
        {
            if (deviceTypeToLanguageFiles.TryGetValue(langPack, out var existsLanguageItems))
            {
                return existsLanguageItems;
            }
        }

        if (_languageCodeToFileNames.TryGetValue(langCode, out var langPackToFileNames))
        {
            if (langPackToFileNames.TryGetValue(langPack, out var name))
            {
                langFileName = name;
            }
            else
            {
                return new List<LangItem>();
            }
        }

        var languageItems = new List<LangItem>();
        var fileName = langFileName;
        if (!File.Exists(fileName))
        {
            return languageItems;
        }

        if (fileName.EndsWith(".xml"))
        {
            return GetXmlLanguagePacks(fileName);
        }

        var pattern = "\"(.+)\" = \"(.+)\"";
        var lines = await File.ReadAllLinesAsync(fileName);
        foreach (var line in lines)
        {
            var match = Regex.Match(line, pattern);
            if (match.Success)
            {
                languageItems.Add(new LangItem(match.Groups[1].Value, match.Groups[2].Value));
            }
        }

        if (!_languages.TryGetValue(langCode, out var langDict))
        {
            langDict = new ConcurrentDictionary<string, List<LangItem>>();
            langDict.TryAdd(langPack, languageItems);
            _languages.TryAdd(langCode, langDict);
        }
        else
        {
            langDict.TryAdd(langPack, languageItems);
        }

        return languageItems;
    }

    private List<LangItem> GetXmlLanguagePacks(string fileName)
    {
        var items = new List<LangItem>();
        var doc = new XmlDocument();
        doc.Load(fileName);
        foreach (var node in doc.DocumentElement.ChildNodes)
        {
            if (node is XmlElement element)
            {
                items.Add(new LangItem(element.GetAttribute("name"), element.InnerText));
            }
        }


        return items;
    }
}