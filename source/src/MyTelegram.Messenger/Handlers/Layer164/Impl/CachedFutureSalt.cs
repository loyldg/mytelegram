// ReSharper disable All

namespace MyTelegram.Messenger.Handlers.Impl;

public class CachedFutureSalt
{
    public long Salt { get; set; }
    public int ValidSince { get; set; }
    public int ValidUntil { get; set; }
}