namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IConfigConverter : ILayeredConverter
{
    IConfig ToConfig(List<DcOption> dcOptions,
        int thisDcId,
        int mediaDcId);
}