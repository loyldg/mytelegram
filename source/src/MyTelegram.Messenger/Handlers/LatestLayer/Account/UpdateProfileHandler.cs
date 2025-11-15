namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Updates user profile.
/// Possible errors
/// Code Type Description
/// 400 ABOUT_TOO_LONG About string too long.
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.updateProfile"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateProfileHandler(ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateProfile, MyTelegram.Schema.IUser>
{
    protected override async Task<IUser> HandleCoreAsync(IRequestInput input, RequestUpdateProfile obj)
    {
        var userId = UserId.Create(input.UserId);
        var command = new UpdateProfileCommand(userId, input.ToRequestInfo(), obj.FirstName, obj.LastName, obj.About);
        await commandBus.PublishAsync(command);
        return null !;
    }
}