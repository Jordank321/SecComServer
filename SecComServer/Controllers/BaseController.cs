using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SecComServer.Controllers
{
    public class BaseController : Controller
    {
        protected string Username => HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}
