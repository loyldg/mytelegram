// ReSharper disable All

using MyTelegram.Messenger.Services.Filters;
using RequestCheckUsername = MyTelegram.Schema.Account.RequestCheckUsername;

namespace MyTelegram.Handlers.Account;

/// <summary>
///     Validates a username and checks availability.
///     <para>Possible errors</para>
///     <code>
///  Code Type Description
///  400 USERNAME_INVALID The provided username is not valid.
///  400 USERNAME_OCCUPIED The provided username is already occupied.
///  400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
///  </code>
///     See <a href="https://corefork.telegram.org/method/account.checkUsername" />
/// </summary>
internal sealed class CheckUsernameHandler : RpcResultObjectHandler<RequestCheckUsername, IBool>,
    Account.ICheckUsernameHandler
{
    private readonly ICuckooFilter _cuckooFilter;

    public CheckUsernameHandler(ICuckooFilter cuckooFilter)
    {
        _cuckooFilter = cuckooFilter;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestCheckUsername obj)
    {
        if (await _cuckooFilter
                .ExistsAsync(
                    Encoding.UTF8.GetBytes($"{MyTelegramServerDomainConsts.UserNameCuckooFilterKey}_{obj.Username}"))
                .ConfigureAwait(false))
            return new TBoolFalse();

        return new TBoolTrue();
    }
}