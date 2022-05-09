// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISavedContact : IObject
{
    string Phone { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    int Date { get; set; }

}
