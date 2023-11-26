namespace CloudTicTacToe.Application.Models;
public class NotFound
{
    public string Identifier { get; init; }

    public string Message => $"Not found entity with id = {Identifier}";

    public NotFound(string identifier)
    {
        Identifier = identifier;
    }

    public NotFound(Guid identifier)
    {
        Identifier = identifier.ToString();
    }
}
