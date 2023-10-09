using MyTelegram.Messenger.Services;

namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IDialogConverter : ILayeredConverter, IHasRequestLayer
{
    IDialogs ToDialogs(GetDialogOutput output/*, IMessageConverter messageConverter, IUserConverter userConverter, IChatConverter chatConverter, IPhotoConverter photoConverter*/);
    IPeerDialogs ToPeerDialogs(GetDialogOutput output/*, IMessageConverter messageConverter, IUserConverter userConverter, IChatConverter chatConverter, IPhotoConverter photoConverter*/);
}