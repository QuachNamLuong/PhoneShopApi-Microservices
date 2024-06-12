using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Auth.Models;
using PhoneShopApi.Auth.Interfaces;
using PhoneShopApi.Auth.Dto.User;
using PhoneShopApi.Auth.Data;
using System.Reflection.Metadata.Ecma335;

namespace PhoneShopApi.Auth.Controllers
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

        [HttpPut]
        [Route("UpdateUserInfo/user/{userId}")]
        public  async Task<IActionResult> UpdateUserInfo(
            [FromRoute] string userId,
            [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest("Canot login now.");



            var user = await _context.Users
                .Where(u => u.Id.Equals(userId))
                .FirstOrDefaultAsync();

            if (user == null) return NotFound("user not found.");


            user.FirstName = updateUserRequestDto.FirstName;
            user.LastName = updateUserRequestDto.LastName;
            user.PhoneNumber = updateUserRequestDto.PhoneNumber;
            user.Email = updateUserRequestDto.Email;
            user.Address = updateUserRequestDto.Address;

            await _context.SaveChangesAsync();

            return Ok(updateUserRequestDto);
        }

    }
}
