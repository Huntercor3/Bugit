using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aspnetserver.Controllers
{
    // https://purple-ground-019dc9c0f.1.azurestaticapps.net/api/user
    [Route("api/user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        // https://purple-ground-019dc9c0f.1.azurestaticapps.net/api/register
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            string email = user.email;
            string password = user.password;

            IdentityUser identityUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            IdentityResult identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
                return Ok(new { identityResult.Succeeded });
            else
            {
                string errorsToRtn = "Register failed with the following errors";

                foreach (var error in identityResult.Errors)
                {
                    errorsToRtn += Environment.NewLine;
                    errorsToRtn += $"Error code: {error.Code} - {error.Description}";
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorsToRtn);
            }
        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            string username = user.email;
            string password = user.password;

            Microsoft.AspNetCore.Identity.SignInResult signInResult =
                await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (signInResult.Succeeded)
            {
                IdentityUser identityUser = await _userManager.FindByNameAsync(username);
                string JSONWebTokenAsString = await GenerateJSONWebToken(identityUser);
                return Ok(JSONWebTokenAsString);
            }
            else
                return Unauthorized(user);
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<string> GenerateJSONWebToken(IdentityUser identityUser)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            IList<string> roleNames = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    null,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}