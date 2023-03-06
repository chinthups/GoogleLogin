using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;

namespace GoogleLogin.Controllers
{
    public class AcountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            string returnUrl = "/Account/UserInfo";
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var firstName = claimsIdentity.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = claimsIdentity.FindFirst(ClaimTypes.Surname)?.Value;

            ViewData["Email"] = email;
            ViewData["FirstName"] = firstName;
            ViewData["LastName"] = lastName;

            return View();
        }


        [HttpPost]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
       
    }
}
