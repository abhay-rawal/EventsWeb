using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Events_Data;
using EventsWeb.Shared.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EventsWeb.Server.Account
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApiSettings _apiSettings;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<ApiSettings> apisettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _apiSettings = apisettings.Value;   
                      
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
                var user = await _userManager.FindByIdAsync(signInRequest.UserName);
                if (user == null)
                {
                    return Unauthorized(new EventsSignInResponse
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Inavalid Authentication"
                    });

                }
                //Get SigninCredentials 
                var signInCredentials = GetSigningCredentials();
                //Get Claims
                var claims = await getClaims(user);
                //Create TokenOptions or can Use TokenDesciptor

                var tokenOptions = new JwtSecurityToken(
                    issuer:_apiSettings.validIssuer,
                    audience : _apiSettings.validAudience,
                    claims : claims,
                    expires : DateTime.UtcNow.AddDays(30),
                    signingCredentials : signInCredentials);  
                
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new EventsSignInResponse()
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    UserDTO = new EventsUser
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber
                    }
                }); 
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

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> getClaims(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email), 
                new Claim("Id",user.Id)
            };
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
