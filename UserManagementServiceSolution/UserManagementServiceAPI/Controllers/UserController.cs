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
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _userService.RegisterUser(userRegisterDTO);
                return Created("User successfully registered", user);
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
                return Ok("User successfully LoggedIn");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserDTO userDTO)
        {
            try
            {
                var user = await _userService.UpdateUser(userId, userDTO);
                if (user == null)
                {
                    return BadRequest("Invalid User ID");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetUserById(int userId)
        {
            try {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound($"No Users found with User ID - {userId}");
                }
                if (user.UserId <= 0)
                {
                    return BadRequest("Invalid User ID");
                }
                return Ok(user);
            }
            catch (Exception)
            {
                throw new Exception($"Couldn't get the User with Id - {userId}");
            }
        }
    }
}
