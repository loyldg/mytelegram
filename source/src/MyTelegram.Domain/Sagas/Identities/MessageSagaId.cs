namespace MyTelegram.Domain.Sagas.Identities;

public class MessageSagaId : MyIdentity<MessageSagaId>, ISagaId
{
    public MessageSagaId(string value) : base(value)
    {
    }
}