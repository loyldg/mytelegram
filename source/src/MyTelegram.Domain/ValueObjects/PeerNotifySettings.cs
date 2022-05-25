namespace MyTelegram.Domain.ValueObjects;

public class PeerNotifySettings : ValueObject
{
    public PeerNotifySettings(
        bool? showPreviews,
        bool? silent,
        int? muteUntil,
        string? sound)
    {
        ShowPreviews = showPreviews;
        Silent = silent;
        MuteUntil = muteUntil;
        Sound = sound;
    }
    public static PeerNotifySettings DefaultSettings { get; } = new(true, false, 0, "default");
    public int? MuteUntil { get; private set; } //= 0;// = int.MaxValue;

    public bool? ShowPreviews { get; private set; } //= true;
    public bool? Silent { get; private set; } //= false;
    public string? Sound { get; private set; } //= "default";
}
