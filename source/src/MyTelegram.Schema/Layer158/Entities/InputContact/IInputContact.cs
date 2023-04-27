// ReSharper disable All

namespace MyTelegram.Schema;

public interface IInputContact : IObject
{
    long ClientId { get; set; }
    string Phone { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
}
