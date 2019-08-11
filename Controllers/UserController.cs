using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Dtos;
using ProAgil.WebAPI.Identity;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config,
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
                    token = GenerateJwtToken(AppUser),
                    UserName = userToReturn
                });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User User)
        {
            return "";
        }
    }
}