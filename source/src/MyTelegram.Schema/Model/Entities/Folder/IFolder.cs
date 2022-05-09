// ReSharper disable All

namespace MyTelegram.Schema;

public interface IFolder : IObject
{
    BitArray Flags { get; set; }
    bool AutofillNewBroadcasts { get; set; }
    bool AutofillPublicGroups { get; set; }
    bool AutofillNewCorrespondents { get; set; }
    int Id { get; set; }
    string Title { get; set; }
    MyTelegram.Schema.IChatPhoto? Photo { get; set; }

}
