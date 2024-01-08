namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public abstract class UserConverterBase : LayeredConverterBase
{
    protected abstract ILayeredUser ToUser(IUserReadModel user);
}