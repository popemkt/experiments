using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<FullStackHeroWebAPIBoilerplate.Infrastructure.Caching.LocalCacheService>
{
    protected override FullStackHeroWebAPIBoilerplate.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<FullStackHeroWebAPIBoilerplate.Infrastructure.Caching.LocalCacheService>.Instance);
}