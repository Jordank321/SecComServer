using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SecComServer.Data;

namespace SecComServer
{
    public static class IdentityExtensions
    {
        public static async Task<ApplicationUser> FindByNameOrEmailAsync
            (this UserManager<ApplicationUser> userManager, string usernameOrEmail, string password)
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

            if(user != null && await userManager.CheckPasswordAsync(user, password))
            {
                return user;
            }

            return null;
        }
    }
}