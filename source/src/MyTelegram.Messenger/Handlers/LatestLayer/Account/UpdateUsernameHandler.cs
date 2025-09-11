namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Changes username for the current user.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
/// See <a href="https://corefork.telegram.org/method/account.updateUsername" />
///</summary>
internal sealed class UpdateUsernameHandler(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    IUsernameHelper usernameHelper
    )
    : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateUsername, MyTelegram.Schema.IUser>
{
    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateUsername obj)
    {
        if (!string.IsNullOrEmpty(obj.Username))
        {
            if (!usernameHelper.IsValidUsername(obj.Username))
            {
                RpcErrors.RpcErrors400.UsernameInvalid.ThrowRpcError();
            }
        }

        var userReadModel=await userAppService.GetAsync(input.UserId);
        var oldUserName = userReadModel.UserName;
        if (string.Equals(obj.Username, oldUserName, StringComparison.OrdinalIgnoreCase))
        {
            RpcErrors.RpcErrors400.UsernameNotModified.ThrowRpcError();
        }

        var command = new SetUserNameCommand(UserNameId.Create(obj.Username.ToLower()),
            input.ToRequestInfo(),
            input.UserId.ToUserPeer(),
            obj.Username,
            oldUserName
            );
        await commandBus.PublishAsync(command);

        return null!;
    }
}
