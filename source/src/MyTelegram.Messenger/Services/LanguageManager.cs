using System.Text.RegularExpressions;
using System.Xml;

namespace MyTelegram.Messenger.Services;

public class LanguageManager : ILanguageManager
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, List<LangItem>>> _languages = new();
    //private readonly Dictionary<string, string> _languageCodeToFileName = new()
    //{
    //    {"en","lang.strings"},
    //    {"zh-hans","tdesktop_zh-hans_v2322778.strings"},
    //};

    private readonly ConcurrentDictionary<string, Dictionary<string, string>> _languageCodeToFileNames = new();
    private readonly string _defaultLanguageFileName = "tdesktop_en_v2309693.strings";
    public LanguageManager()
    {
        _languageCodeToFileNames.TryAdd("en", new Dictionary<string, string>()
        {
            {"tdesktop","tdesktop_en_v2309693.strings"},
            {"ios","ios_en_v10761094.strings"},
            {"android","android_en_v33978726.xml"},
            {"weba","weba_en_v2254.strings"}

        });

        _languageCodeToFileNames.TryAdd("zh-hans", new Dictionary<string, string>()
        {
            {"tdesktop","tdesktop_zh-hans_v2322778.strings"},
            {"ios","ios_zh-hans_v10785061.strings"},
            {"macos","macos_zh-hans_v938077.strings"},
            {"android","android_zh-hans_v34553126.xml"},
            {"weba","weba_zh-hans_v8271.strings"}

        });
    }

    public async Task<List<LangItem>> GetAllLangPacksAsync(string langCode, string langPack = "tdesktop")
    {
        var langFileName = _defaultLanguageFileName;

        if (_languages.TryGetValue(langCode, out var dict))
        {
            if (dict.TryGetValue(langPack, out var existsLanguageItems))
            {
                Console.WriteLine($"###Get lang packs:langCode:{langCode} {langPack} count={existsLanguageItems.Count}");
                return existsLanguageItems;
            }
            //else
            //{
            //    Console.WriteLine($"get langPack failed:{langPack}");
            //    //langFileName = dict.First().Key;
            //    return new List<LangItem>();
            //}
        }


        if (_languageCodeToFileNames.TryGetValue(langCode, out var langPackToFileNames))
        {
            if (langPackToFileNames.TryGetValue(langPack, out var name))
            {
                langFileName = name;
            }
            else
            {
                Console.WriteLine($"get langPack failed2:{langPack}");

                return new List<LangItem>();
            }
        }

        var languageItems = new List<LangItem>();
        var fileName = Path.Combine(AppContext.BaseDirectory, "Langs", langFileName);
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

        Console.WriteLine($"Get all languages success,count={languageItems.Count}");
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
                items.Add(new LangItem(element.GetAttribute("name"),element.InnerText));
            }
        }


        return items;
    }
}