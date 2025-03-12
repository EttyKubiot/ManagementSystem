using Microsoft.AspNetCore.Mvc;
using ManagementSystem.Services;
using ManagementSystem.Data.Entities;
using AutoMapper;
using ManagementSystem.Core.DTOs;
using ManagementSystem.API.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;



namespace ManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);
            return Ok(userDTOs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserPostModel user)
        {
            var userToAdd = new User { FirstName = user.FirstName, LastName = user.LastName, Email = user.Email, RoleId = user.RoleId };
            // בעתיד נשתמש בהצפנה

            var newUser = await _userService.CreateUserAsync(userToAdd, user.PasswordHash);
            return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, _mapper.Map<UserDTO>(newUser));
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("This is a test error!");
        }


    }
}

