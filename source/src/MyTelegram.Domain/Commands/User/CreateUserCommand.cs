using System.Text;

namespace MyTelegram.Domain.Commands.User;

public class CreateUserCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public CreateUserCommand(UserId aggregateId,
        RequestInfo request,
        long userId,
        long accessHash,
        string phoneNumber,
        string firstName,
        string? lastName,
        bool bot = false) : base(aggregateId, request)
    {
        UserId = userId;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        AccessHash = accessHash;
        Bot = bot;
    }

    public long AccessHash { get; }
    public bool Bot { get; }
    public string FirstName { get; }
    public string? LastName { get; } 
    public string PhoneNumber { get; }
    public long UserId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        if (Bot)
        {
            yield return BitConverter.GetBytes(UserId);
            yield return Encoding.UTF8.GetBytes(FirstName);
        }
        else
        {
            yield return BitConverter.GetBytes(Request.ReqMsgId);
            yield return Encoding.UTF8.GetBytes(PhoneNumber);
        }
    }
}
