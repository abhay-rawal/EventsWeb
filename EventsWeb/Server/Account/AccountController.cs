using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Server.Account
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] EventsSignUpRequest signUpRequest)
        {
            if(signUpRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
           
            return View();
        }
    }
}
