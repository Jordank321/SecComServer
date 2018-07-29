using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        [HttpPost] 
        public async Task<IActionResult> Register([FromBody]Registration registration) 
        { 
            var user = new ApplicationUser { UserName = registration.Username }; 
            var result = await _userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded) return BadRequest();

            await _signInManager.SignInAsync(user, true); 
            return Ok();
        }
    }

    public class Registration
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
