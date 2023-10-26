using Microsoft.AspNetCore.Identity;

namespace Inventory_API.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
