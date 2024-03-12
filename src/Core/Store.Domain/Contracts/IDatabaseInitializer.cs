namespace Store.Domain.Contracts;

public interface IDatabaseInitializer
{
    Task InitializeDatabaseAsync(CancellationToken cancellationToken = default);
}
