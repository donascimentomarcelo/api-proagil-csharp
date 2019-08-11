using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProAgil.WebAPI.Dtos;
using ProAgil.WebAPI.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UsersController(IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUser(UserDto UserDto)
        {
            return Ok(UserDto);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto UserDto)
        {
            var User = _mapper.Map<User>(UserDto);
            var Result = await _userManager.CreateAsync(User, UserDto.Password);
            var UserToReturn = _mapper.Map<UserDto>(User);
            if (Result.Succeeded) 
            {
                return Created("GetUser", UserToReturn);
            }
            return BadRequest(Result.Errors);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto UserLoginDto)
        {
            var User = await _userManager.FindByNameAsync(UserLoginDto.UserName);
            var Result = await _signInManager.CheckPasswordSignInAsync(User, UserLoginDto.Password, false);

            if (Result.Succeeded)
            {
                var AppUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == UserLoginDto.UserName);
                var userToReturn = _mapper.Map<UserLoginDto>(AppUser);

                return Ok(new {
                    token = GenerateJwtToken(AppUser).Result,
                    UserName = userToReturn
                });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User User)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(ClaimTypes.Name, User.UserName)
            };

            var roles = await _userManager.GetRolesAsync(User);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}