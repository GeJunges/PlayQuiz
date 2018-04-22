using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    public class Credentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Produces("application/json")]
    [Route("api/account")]
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Credentials credentials)
        {
            var user = new IdentityUser
            {
                UserName = credentials.Email,
                Email = credentials.Password
            };

            var result = await _userManager.CreateAsync(user, credentials.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var token = CreateToken(user);

            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var result = await _signInManager.PasswordSignInAsync(credentials.Email, credentials.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(credentials.Password);
            var token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(IdentityUser user) {

            var claims = new Claim[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)};

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretFrase123456$"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}