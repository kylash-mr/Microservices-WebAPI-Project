﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserManagementServiceAPI.Interfaces;
using UserManagementServiceAPI.Models.DTOs;

namespace UserManagementServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDTO>> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _userService.RegisterUser(userRegisterDTO);
                if (user == null)
                {
                    return Unauthorized();
                }
                return Created("User registration is successful",user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userService.LoginUser(userLoginDTO);
                return Ok("User Login is successful");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetUserById(string userId)
        {
            try {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("No Users found with that Username");
                }
                return Ok(user);
            }
            catch (Exception)
            {
                throw new Exception("Couldn't get the User details");
            }
        }
    }
}
