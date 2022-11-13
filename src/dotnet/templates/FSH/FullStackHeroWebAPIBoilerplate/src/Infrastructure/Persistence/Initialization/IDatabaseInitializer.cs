using FullStackHeroWebAPIBoilerplate.Infrastructure.Multitenancy;

namespace FullStackHeroWebAPIBoilerplate.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForTenantAsync(FSHTenantInfo tenant, CancellationToken cancellationToken);
}