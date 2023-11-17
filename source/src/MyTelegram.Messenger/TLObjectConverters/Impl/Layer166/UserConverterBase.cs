namespace MyTelegram.Messenger.TLObjectConverters.Impl.Layer166;

public abstract class UserConverterBase : LayeredConverterBase
{
    protected abstract ILayeredUser ToUser(IUserReadModel user);
}