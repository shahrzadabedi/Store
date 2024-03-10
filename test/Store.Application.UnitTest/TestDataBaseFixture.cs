using System;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure;

namespace Store.Application.UnitTest;
public class TestDatabaseFixture : IDisposable
{
    private readonly string _connectionString;

    private readonly DbContextOptions<DataContext> _options;

    public TestDatabaseFixture()
    {
        var dbName = Guid.NewGuid().ToString();

        _connectionString = $"server=.; Initial Catalog=TeamDB_{dbName};User ID=sa;Password=123;TrustServerCertificate=True;";

        _options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(_connectionString)
            .Options;
        using (var context = new DataContext(_options))
        {
            context.Database.EnsureCreated();
        }
    }

    public DataContext CreateContext()
    {
        return new DataContext(_options);
    }

    public void Dispose()
    {
        using (var context = new DataContext(_options))
        {
            context.Database.EnsureDeleted();
        }
    }
}
