namespace MyTelegram.Messenger.Handlers.LatestLayer.Stickers;
/// <summary>
/// Check whether the given short name is available
/// Possible errors
/// Code Type Description
/// 400 SHORT_NAME_INVALID The specified short name is invalid.
/// 400 SHORT_NAME_OCCUPIED The specified short name is already in use.
/// <para><c>See <a href="https://corefork.telegram.org/method/stickers.checkShortName"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CheckShortNameHandler : RpcResultObjectHandler<MyTelegram.Schema.Stickers.RequestCheckShortName, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stickers.RequestCheckShortName obj)
    {
        throw new NotImplementedException();
    }
}