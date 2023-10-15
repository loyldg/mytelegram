// ReSharper disable All

using MyTelegram.Messenger.Services.Filters;

namespace MyTelegram.Handlers.Account;

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
internal sealed class UpdateUsernameHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateUsername, MyTelegram.Schema.IUser>,
    Account.IUpdateUsernameHandler
{
    private readonly ICommandBus _commandBus;
    private readonly ICuckooFilter _cuckooFilter;

    public UpdateUsernameHandler(ICommandBus commandBus,
        ICuckooFilter cuckooFilter)
    {
        _commandBus = commandBus;
        _cuckooFilter = cuckooFilter;
    }

    protected override async Task<IUser> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateUsername obj)
    {
        if (await _cuckooFilter
                .ExistsAsync(Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{obj.Username}"))
                .ConfigureAwait(false))
        {
            RpcErrors.RpcErrors400.UsernameOccupied.ThrowRpcError();
        }

        var command = new SetUserNameCommand(UserNameId.Create(obj.Username),
            input.ToRequestInfo(),
            input.UserId,
            PeerType.User,
            input.UserId,
            obj.Username);
        await _commandBus.PublishAsync(command, default);
        return null!;
    }
}
