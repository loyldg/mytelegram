namespace MyTelegram.Schema;

public interface IHasSubQuery
{
    IObject Query { get; set; }
}