using System.Security.Claims;

namespace FullStackHeroWebAPIBoilerplate.Infrastructure.Auth;

public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}