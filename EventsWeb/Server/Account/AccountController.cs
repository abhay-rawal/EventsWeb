using Events_Data;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventsWeb.Server.Account
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        //For SignUp Request
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] EventsSignUpRequest signUpRequest)
        {
            if(signUpRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            //Create a new Application User
            var User = new ApplicationUser
            {
                UserName = signUpRequest.Email,
                Email = signUpRequest.Email,
                Name = signUpRequest.Name,
                PhoneNumber = signUpRequest.PhoneNumber,
                EmailConfirmed = true

            };
            //If roles are not created, Create Roles
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_Admin));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            //Create User Inside Table
            var result = await _userManager.CreateAsync(User,signUpRequest.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new EventsSignUpResponse()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }
            //Assign A role to user
            var roleResult = await _userManager.AddToRoleAsync(User, "Admin");
            //Check if the operation was succesful
            if (!roleResult.Succeeded)
            {
                return BadRequest(new EventsSignUpResponse()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            return StatusCode(201);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] EventsSignInRequest signInRequest)
        {
            if (signInRequest == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            //SignInUser
            var result = await _signInManager.PasswordSignInAsync(signInRequest.UserName,signInRequest.Password,false,false);
            //Check if it Succeded
            if (result.Succeeded)
            {
                var user = _userManager.FindByIdAsync(signInRequest.UserName);
                if (user == null)
                {
                    return Unauthorized(new EventsSignInResponse
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Inavalid Authentication"
                    });

                }
                //Todo: Remove
                return Ok();
                //Valid and need to login
            }
            else
            {
                return Unauthorized(new EventsSignInResponse
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Inavalid Authentication"
                });

            }
        }
    }
}
