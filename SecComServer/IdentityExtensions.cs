using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SecComServer.Data;

namespace SecComServer
{
    public static class IdentityExtensions
    {
        public static async Task<ApplicationUser> FindByNameOrEmailAsync
            (this UserManager<ApplicationUser> userManager, string usernameOrEmail)
        {
            var username = usernameOrEmail;
            if (usernameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(usernameOrEmail);
                if (userForEmail != null)
                {
                    username = userForEmail.UserName;
                }
            }
            var user = await userManager.FindByNameAsync(username);

            return user;
        }
    }
}