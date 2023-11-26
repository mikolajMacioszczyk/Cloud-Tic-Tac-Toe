using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudTicTacToe.Infrastructure;
public sealed class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly TicTacToeContext _context;
    private readonly ILogger<UnitOfWork> _logger;

    public ICellRepository CellRepository { get; }

    public IPlayerRepository PlayerRepository { get; }

    public IGameBoardRepository GameBoardRepository { get; }

    public UnitOfWork(TicTacToeContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;

        CellRepository = new CellRepository(context);
        PlayerRepository = new PlayerRepository(context);
        GameBoardRepository = new GameBoardRepository(context);
    }

    public async Task<(bool ChangesMade, IEnumerable<BaseDomainModel> EntitiesWithErrors)> SaveChangesAsync()
    {
        bool saved = false;
        bool changesMade = false;
        List<BaseDomainModel> entitiesWithErrors = new();

        while (!saved)
        {
            try
            {
                changesMade = await _context.SaveChangesAsync() > 0;
                saved = true;
            }
            // Will be thrown if somebody else modified entity in the meantime
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogDebug("[{Now}]: Encountered concurrency exception...", DateTime.Now);

                // Get entries that caused an exception
                foreach (var entry in ex.Entries)
                {
                    // Replace values with the ones recently saved to bypass this exception
                    var databaseValues = await entry.GetDatabaseValuesAsync();

                    _logger.LogDebug("[{Now}]: Replacing values for entity from database: {Values}",
                        DateTime.Now, string.Join(Environment.NewLine, entry.CurrentValues.Properties.Select(p => $"{p.Name}: {entry.CurrentValues[p.Name]} -> {databaseValues?[p.Name]}")));

                    entry.CurrentValues.SetValues(databaseValues!);
                    entry.OriginalValues.SetValues(databaseValues!);

                    entitiesWithErrors.Add(entry.Entity as BaseDomainModel
                        ?? throw new InvalidOperationException($"The entity with ID {entry.CurrentValues["Id"]} is not a {nameof(BaseDomainModel)}"));
                }
            }
            // Will be thrown if deleting the entity violates database constraints
            catch (DbUpdateException ex)
            {
                _logger.LogDebug("[{Now}]: Encountered db update exception...", DateTime.Now);

                // Get entries that caused an exception
                foreach (var entry in ex.Entries)
                {
                    _logger.LogDebug("[{Now}]: Detaching an entity with ID {Id}",
                        DateTime.Now, entry.CurrentValues["Id"]);

                    // Detach such entry and leave it unchanged
                    entry.State = EntityState.Detached;

                    entitiesWithErrors.Add(entry.Entity as BaseDomainModel
                        ?? throw new InvalidOperationException($"The entity with ID {entry.CurrentValues["Id"]} is not a {nameof(BaseDomainModel)}"));
                }
            }
        }

        return (changesMade, entitiesWithErrors);
    }

    private bool disposed = false;

    public void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _context.Dispose();

        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

