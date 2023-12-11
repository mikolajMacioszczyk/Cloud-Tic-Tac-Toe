using CloudTicTacToe.Application.Interfaces;

namespace CloudTicTacToe.Application.Services
{
    public class GameConnectionService : IGameConnectionService
    {
        // TODO: ConcurrentDictionary
        private static readonly Dictionary<Guid, List<string>> GamesToConnectionIds = new();

        public void AddGameConnnectionId(Guid gameId, string connnectionId)
        {
            lock (GamesToConnectionIds)
            {
                if (!GamesToConnectionIds.ContainsKey(gameId))
                {
                    GamesToConnectionIds[gameId] = new List<string>();
                }
                GamesToConnectionIds[gameId].Add(connnectionId);
            }
        }

        public Guid? GetGameByConnnectionId(string connectionId)
        {
            lock (GamesToConnectionIds)
            {
                return GamesToConnectionIds
                    .Where(x => x.Value.Contains(connectionId))
                    .Select(x => x.Key)
                    .FirstOrDefault();
            }
        }

        public void RemoveUserFromGame(Guid gameId, string connectionId)
        {
            lock (GamesToConnectionIds)
            {
                if (GamesToConnectionIds.ContainsKey(gameId))
                {
                    GamesToConnectionIds[gameId].Remove(connectionId);
                    if (GamesToConnectionIds[gameId].Count == 0)
                    {
                        GamesToConnectionIds.Remove(gameId);
                    }
                }
            }
        }
    }
}
