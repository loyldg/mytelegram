namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer164;

public abstract class UserConverterBase : LayeredConverterBase
{
    protected abstract ILayeredUser ToUser(IUserReadModel user);
}