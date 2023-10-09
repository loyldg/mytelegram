using System.Text;

namespace MyTelegram.Domain.Commands.User;

public class CreateUserCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public CreateUserCommand(UserId aggregateId,
        RequestInfo requestInfo,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        string? userName = null,
        bool bot = false) : base(aggregateId, requestInfo)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        AccessHash = accessHash;
        Bot = bot;
    }

    public long AccessHash { get; }
    public bool Bot { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string? UserName { get; }
    public string PhoneNumber { get; }
    public long UserId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        if (Bot)
        {
            yield return BitConverter.GetBytes(UserId);
            yield return Encoding.UTF8.GetBytes(UserName!);
        }
        else
        {
            yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
            yield return Encoding.UTF8.GetBytes(PhoneNumber);
        }
    }
}