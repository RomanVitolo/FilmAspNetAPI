using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FinalFilmProject.DTOS;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FinalFilmProject.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : CustomBaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountsController(ApplicationDbContext context, IMapper mapper, 
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IConfiguration configuration) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser {UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return await BuildToken(model);
            else return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded) return await BuildToken(model);
            else return BadRequest("Invalid login attempt");
        }

        [HttpPost("renovateToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> Renovate()
        {
            var userInfo = new UserInfo
            {
                Email = HttpContext.User.Identity.Name
            };
            return await BuildToken(userInfo);
        }

        [HttpGet("users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PageDTO pageDto)
        {
            var queryable = _context.Users.AsQueryable();
            queryable = queryable.OrderBy(x => x.Email);
            return await Get<IdentityUser, UserDTO>(pageDto);
        }

        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            return await _context.Roles.Select(x => x.Name).ToListAsync();
        }

        [HttpPost("assignRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RolAssignment(EditRolDTO editRolDto)
        {
            var user = await _userManager.FindByIdAsync(editRolDto.UserId);
            
            if (user == null) return NotFound();

            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRolDto.RolName));
            return NoContent();
        }

        [HttpPost("removeRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RemoveRol(EditRolDTO editRolDto)
        {
            var user = await _userManager.FindByIdAsync(editRolDto.UserId);
            if (user == null) return NotFound();

            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRolDto.RolName));
            return NoContent();
        }

        private async Task<UserToken> BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Email),
                new Claim(ClaimTypes.Email, userInfo.Email),
            };

            var identityUser = await _userManager.FindByEmailAsync(userInfo.Email);
            
            claims.Add(new Claim(ClaimTypes.NameIdentifier, identityUser.Id));

            var claimsDB = await _userManager.GetClaimsAsync(identityUser);
            
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };    
        }
    }
}