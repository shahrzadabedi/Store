using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;

namespace Store.Infrastructure.Persistence;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly DataContext _context;

    public DatabaseInitializer(DataContext context)
    {
        _context = context;
    }

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken = default)
    {
        //later logging can be added
        if (!_context.Database.GetMigrations().Any())
            return;

        if (!(await _context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            return;

        await _context.Database.MigrateAsync(cancellationToken);
    }
}
