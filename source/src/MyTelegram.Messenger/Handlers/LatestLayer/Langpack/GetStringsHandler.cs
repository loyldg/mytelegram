namespace MyTelegram.Messenger.Handlers.LatestLayer.Langpack;

///<summary>
/// Get strings from a language pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getStrings" />
///</summary>
internal sealed class GetStringsHandler(ILanguageCacheService languageCacheService, ILangPackStringConverterService langPackStringConverterService) : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetStrings, TVector<MyTelegram.Schema.ILangPackString>>
{
    protected override async Task<TVector<ILangPackString>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetStrings obj)
    {
        var foundLanguageItems = await languageCacheService.GetLanguageTextsAsync(obj.LangCode, obj.LangPack, obj.Keys);
        var langPackStrings = langPackStringConverterService.ToLangPackStrings(foundLanguageItems, input.Layer);

        // 3. Identify which keys were NOT found and add them as "Deleted".
        if (foundLanguageItems.Count != obj.Keys.Count)
        {
            // Create a HashSet of found keys for efficient lookups (O(1) complexity).
            var foundKeys = foundLanguageItems.Select(item => item.Key).ToHashSet();

            foreach (var requestedKey in obj.Keys)
            {
                // If the requested key is not in our set of found keys, it's missing.
                if (!foundKeys.Contains(requestedKey))
                {
                    langPackStrings.Add(new TLangPackStringDeleted
                    {
                        Key = requestedKey
                    });
                }
            }
        }

        return langPackStrings;
    }
}
