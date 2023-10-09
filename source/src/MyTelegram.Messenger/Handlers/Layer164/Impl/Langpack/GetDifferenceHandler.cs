// ReSharper disable All

namespace MyTelegram.Handlers.Langpack;

///<summary>
/// Get new strings in language pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getDifference" />
///</summary>
internal sealed class GetDifferenceHandler : RpcResultObjectHandler<MyTelegram.Schema.Langpack.RequestGetDifference, MyTelegram.Schema.ILangPackDifference>,
    Langpack.IGetDifferenceHandler
{
    protected override Task<MyTelegram.Schema.ILangPackDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Langpack.RequestGetDifference obj)
    {
        throw new NotImplementedException();
    }
}
