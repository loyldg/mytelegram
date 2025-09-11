namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;

///<summary>
/// Check whether the given short name is available
/// <para>Possible errors</para>
/// Code Type Description
/// 400 SHORT_NAME_INVALID The specified short name is invalid.
/// 400 SHORT_NAME_OCCUPIED The specified short name is already in use.
/// See <a href="https://corefork.telegram.org/method/stickers.checkShortName" />
///</summary>
internal sealed class CheckShortNameHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestCheckShortName, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stickers.RequestCheckShortName obj)
    {
        throw new NotImplementedException();
    }
}
