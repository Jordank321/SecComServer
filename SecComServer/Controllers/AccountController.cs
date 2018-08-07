using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecComServer.Data;

namespace SecComServer.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        [HttpPost] 
        public async Task<IActionResult> Register([FromBody]AccountChallenge challenge) 
        { 
            var user = new ApplicationUser { UserName = challenge.Username }; 
            var result = await _userManager.CreateAsync(user, challenge.Password);

            if (!result.Succeeded) return BadRequest();

            await _signInManager.SignInAsync(user, true); 
            return Ok();
        }

        [HttpPost] 
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]AccountChallenge challenge) 
        { 
            var user = await _userManager.FindByNameOrEmailAsync(challenge.Username);

            if (user == null) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(user, challenge.Password, true, true);
            return result.Succeeded ? (IActionResult)Ok() : Forbid();
        }
    }

    public class AccountChallenge
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
