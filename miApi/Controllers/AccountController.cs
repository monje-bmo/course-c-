using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Dtos.Account;
using miApi.Interfaces;
using miApi.models;
using miApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace miApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITOkenServices _token;
        private readonly SignInManager<AppUser> _signManager; 
        public AccountController(UserManager<AppUser> userManager, ITOkenServices token, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _token = token;
            _signManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid) return BadRequest();

            var user = 
                await _userManager
                        .Users
                        .FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        
            if(user == null) return NotFound("Invalid username!");

            var result = await _signManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if(!result.Succeeded) return Unauthorized("Username not found! and/or password incorrect.");

            return Ok(
                new NewUserDto
                {
                 UserName = user.UserName,
                 Email = user.Email,
                 Token = _token.CreateToken(user)   
                }
            );

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _token.CreateToken(appUser)
                            }
                        );
                    }
                    else 
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}