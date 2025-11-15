namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;
/// <summary>
/// Can only be used by TSF members to obtain internal information.
/// Possible errors
/// Code Type Description
/// 403 USER_INVALID Invalid user provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/help.getUserInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetUserInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetUserInfo, MyTelegram.Schema.Help.IUserInfo>
{
    protected override Task<MyTelegram.Schema.Help.IUserInfo> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Help.RequestGetUserInfo obj)
    {
        throw new NotImplementedException();
    }
}