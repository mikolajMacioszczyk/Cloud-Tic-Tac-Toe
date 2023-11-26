namespace CloudTicTacToe.Application.Models;
public class Failure
{
    public string Message { get; init; }

    public Failure(string message)
    {
        Message = message;
    }
}
