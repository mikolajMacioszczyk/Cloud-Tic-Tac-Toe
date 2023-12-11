namespace CloudTicTacToe.Application.Interfaces
{
    public interface IGameConnectionService
    {
        void AddGameConnnectionId(Guid gameId, string connnectionId);

        Guid? GetGameByConnnectionId(string connectionId);

        void RemoveUserFromGame(Guid gameId, string connectionId);
    }
}
