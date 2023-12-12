using Libs.Dtos;
using Libs.DTOs;
using Libs.Models;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;

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
        public async Task<IActionResult> AddAccount([FromBody] AccountDTO registerRequest)
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
                        Status = true
                    };
                    var isCreate = await _userManager.CreateAsync(newUser, registerRequest.Password);
                    await _userManager.AddToRoleAsync(newUser, registerRequest.Role);
                    return Ok(newUser);
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
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO passwordDTO)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(passwordDTO.Id);
                _accountRepository.HanldeChange(user);

                if (user == null)
                {
                    return BadRequest("No User");

                }
                else if(!await _userManager.CheckPasswordAsync(user, passwordDTO.Password))
                {
                    return BadRequest("Invalid Password");
                }
                var result = await _userManager.ChangePasswordAsync(user, passwordDTO.Password, passwordDTO.NewPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating account: {ex.Message}");
            }
        }

        [HttpPatch("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountShowDTo updatedUser)
        {
            try
            {
                User existingUser = await _userManager.FindByIdAsync(updatedUser.Id);
                if (existingUser == null)
                {
                    return NotFound("User not found");
                }
                _accountRepository.HanldeChange(existingUser);

                existingUser.DateBirthday = updatedUser.DateBirthday;
                existingUser.Email = updatedUser.Email;
                existingUser.FullName = updatedUser.FullName;
                existingUser.Status = updatedUser.Status;

                var existingRoles = await _userManager.GetRolesAsync(existingUser);
                await _userManager.RemoveFromRolesAsync(existingUser, existingRoles);
                foreach (var role in updatedUser.Role)
                {
                    await _userManager.AddToRoleAsync(existingUser, role);
                }
                _accountRepository.UpdateAccount(existingUser);
                _accountRepository.Save();

                return Ok("Account updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating account: {ex.Message}");
            }
        }

        [HttpPatch("ChangeStatus")]
        public IActionResult ChangeStatus(string id, bool status)
        {
            try
            {
                User existingUser = _accountRepository.GetById(id);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }
                existingUser.Status = status;
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
        public async Task<IActionResult> ShowAccount()
        {
            try
            {
                List<User> users = _accountRepository.getAll();
                List<AccountShowDTo> usersDTO = new List<AccountShowDTo>();

                foreach (var user in users)
                {
                    AccountShowDTo userDTO = new AccountShowDTo
                    {
                        Id = user.Id,
                        DateBirthday = user.DateBirthday,
                        Email = user.Email,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        Status = user.Status,
                        Role = (await _userManager.GetRolesAsync(user)).ToList()
                    };

                    usersDTO.Add(userDTO);
                }

                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving accounts: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("DetailAccount")]
        public async Task<IActionResult> DetailAccount(string id)
        {
            try
            {
                User user = _accountRepository.GetById(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }
                AccountShowDTo userDTO = new AccountShowDTo
                {
                    Id = user.Id,
                    DateBirthday = user.DateBirthday,
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Status = user.Status,
                    Role = (await _userManager.GetRolesAsync(user)).ToList()
                };
                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving account details: {ex.Message}");
            }
        }

        [HttpGet("GetPagedAccounts")]
        public IActionResult GetPagedAccounts(int page = 1, int pageSize = 10)
        {
            try
            {
                // Sử dụng phương thức GetPagedUsers từ AccountRepository để lấy danh sách người dùng phân trang
                var pagedUsers = _accountRepository.GetPagedUsers(page, pageSize);

                // Lấy tổng số lượng người dùng để tính toán số trang
                var totalUserCount = _accountRepository.getAll().Count();
                var totalPage = (int)Math.Ceiling(totalUserCount / (double)pageSize);
                // Trả về kết quả
                return Ok(new
                {
                    TotalCount = totalUserCount,
                    PageSize = pageSize,
                    CurrentPage = page,
                    TotalPage = totalPage,
                    Users = pagedUsers
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving paged accounts: {ex.Message}");
            }
        }
        [HttpGet("getAllRoles")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return Ok(roles);
        }
    }
}
