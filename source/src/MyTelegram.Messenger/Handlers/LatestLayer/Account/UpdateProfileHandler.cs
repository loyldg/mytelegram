namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Updates user profile.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ABOUT_TOO_LONG About string too long.
/// 400 FIRSTNAME_INVALID The first name is invalid.
/// See <a href="https://corefork.telegram.org/method/account.updateProfile" />
///</summary>
internal sealed class UpdateProfileHandler(ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateProfile, MyTelegram.Schema.IUser>
{
    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        RequestUpdateProfile obj)
    {
        var userId = UserId.Create(input.UserId);
        var command = new UpdateProfileCommand(userId,
            input.ToRequestInfo(),
            obj.FirstName,
            obj.LastName,
            obj.About);
        await commandBus.PublishAsync(command);

        return null!;
    }
}
