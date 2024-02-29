namespace MyTelegram.Messenger.Services;

public interface ILanguageManager
{
    Task<List<LangItem>> GetAllLangPacksAsync(string langCode, string langPack);
    string GetDefaultLangPack(IRequestInput requestInput);
}