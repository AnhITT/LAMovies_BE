using Libs.DTOs;
using Libs.Models;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(IAccountRepository account, UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this._accountRepository = account;
            this._userManager = userManager;
            this._roleManager = roleManager;

        }

        [HttpPost("AddAccount")]
        public async Task<IActionResult> AddAccount([FromBody] RegistrationDTO registerRequest)
        {
            if (ModelState.IsValid)
            {
                var checkUserName = await _userManager.FindByNameAsync(registerRequest.UserName);
                if (checkUserName != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Error = new List<string>()
                        {
                            "Username already exist"
                        }
                    });
                }
                var passwordValidator = new PasswordValidator<User>();
                var result = await passwordValidator.ValidateAsync(_userManager, null, registerRequest.Password);

                if (result.Succeeded)
                {
                    var newUser = new User()
                    {
                        UserName = registerRequest.UserName,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        Email = registerRequest.Email,
                        FullName = registerRequest.FullName,
                        DateBirthday = registerRequest.DateBirthday,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var isCreate = await _userManager.CreateAsync(newUser, registerRequest.Password);
                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    if (await _roleManager.RoleExistsAsync("User"))
                        await _userManager.AddToRoleAsync(newUser, "User");
                    return Ok("Tạo tài khoản thành công");
                }
                else
                {
                    // Mật khẩu không đủ mạnh
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Error = result.Errors.Select(error => error.Description).ToList()
                    });
                }
            }
            return BadRequest(new AuthResult()
            {
                Error = new List<string>()
                {
                     "Invalid Payload"
                },
                Result = false
            });
        }

        [HttpPut("UpdateAccount/{id}")]
        public IActionResult UpdateAccount(string id, [FromBody] User user)
        {
            try
            {
                User existingUser = _accountRepository.GetById(id);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                existingUser.UserName = user.UserName; // Update other properties as needed
                _accountRepository.UpdateAccount(existingUser);
                _accountRepository.Save();

                return Ok("Account updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating account: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeleteAccount")]
        public IActionResult DeleteAccount(string id)
        {
            try
            {
                User user = _accountRepository.GetById(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                _accountRepository.DeleteAccount(user);
                _accountRepository.Save();

                return Ok("Account deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting account: {ex.Message}");
            }
        }

        [HttpGet("ShowAccount")]
        public IActionResult ShowAccount()
        {
            try
            {
                List<User> users = _accountRepository.getAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving accounts: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("DetailAccount")]
        public IActionResult DetailAccount(string id)
        {
            try
            {
                User user = _accountRepository.GetById(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving account details: {ex.Message}");
            }
        }
    }
}
