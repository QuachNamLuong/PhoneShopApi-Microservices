using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneShopApi.Data;
using PhoneShopApi.Models;
using PhoneShopApi.Dto.User;
using PhoneShopApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PhoneShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(
        PhoneShopDbContext context,
        UserManager<User> userManager,
        ITokenService tokenService,
        SignInManager<User> signInManager) : Controller
    {
        private readonly PhoneShopDbContext _context = context; 
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<User> _signInManager = signInManager;

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Cannot register now.");

                var user = new User
                {
                    FirstName = registerDto.FirstName,
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    Address = registerDto.Address,
                    PhoneNumber = registerDto.PhoneNumber,
                    Cart = new Cart(),
                };

                var createdUser = await _userManager.CreateAsync(user, registerDto.Password);
                if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);

                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

                return Ok(
                    new NewUserDto
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Address = user.Address,
                        FirstName = user.FirstName,
                        PhoneNumber = user.PhoneNumber,
                        Token = _tokenService.CreateToken(user)
                    });
            }
            catch (Exception e)
            {
                return StatusCode(500, e);            
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest("Canot login now.");

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);
            if (user == null) return Unauthorized("Invalid username!.");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Username or Password incorrect.");

            return Ok(new NewUserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,                
                Token = _tokenService.CreateToken(user),
                Address = user.Address,
                FirstName = user.FirstName,
                PhoneNumber = user.PhoneNumber
            });
        }
    }
}
