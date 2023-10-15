// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get strings from a language pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getStrings" />
///</summary>
internal sealed class GetStringsHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetStrings, TVector<MyTelegram.Schema.ILangPackString>>,
    Langpack.IGetStringsHandler
{
    protected override Task<TVector<ILangPackString>> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetStrings obj)
    {
        foreach (var objKey in obj.Keys)
        {
            Console.WriteLine($"Lang key:{objKey}");
        }

        var r = new TVector<ILangPackString>(obj.Keys.Select(p => new TLangPackString
        {
            Key = p,
            Value = p
        }));

        return Task.FromResult(r);
    }
}
