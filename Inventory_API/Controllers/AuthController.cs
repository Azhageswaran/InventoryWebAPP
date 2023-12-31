﻿using Inventory_API.IRepository;
using Inventory_API.Models.Dtos.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //Post our admins 
        ///api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult =  await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add Roles to this User
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any() )
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was Registered Sucessfully! Please Login. ");
                    }
                }              
            }
            return BadRequest("Something went wrong");
        }

        //Post login
        // /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
               var chechPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(chechPasswordResult)
                {
                    //Get Roles for this user
                    var roles =  await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                            Roles = roles.First().ToString()

                        };
                        return Ok(response);
                    
                    }
                }
            }
            //user null 
            return BadRequest("UserName or Password incorrect");
        }


    }

    
}
