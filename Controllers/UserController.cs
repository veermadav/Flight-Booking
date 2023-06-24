using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using User_Management.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Interfaces;

namespace User_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly AuthenticationContext _authContext;
        private readonly IConfiguration _configuration;
       
        private readonly ICheckUsrObj _checkUsrObj;

        public UserController(AuthenticationContext authenticationContext, IConfiguration configuration, ICheckUsrObj checkUsrObj)
        {
            _authContext = authenticationContext;
            _configuration = configuration;
            _checkUsrObj = checkUsrObj;
        }
        [HttpPost("Authenticate")]
        //POST : /api/ApplicationUser/Register
        public async Task<IActionResult> Authenticate([FromBody] TblUser userObj)
        {
            if (userObj == null)
                return BadRequest();
            var user = await _authContext.tblUsers
                .FirstOrDefaultAsync(x => x.UserName == userObj.UserName);
            if (user == null)
                return NotFound(new { Message = "User Not Found" });
            if (!PasswordHasher.VerifyPassword(userObj.UsrPassword, user.UsrPassword))
                return BadRequest(new { Message = "Password is Incorrect" });
            //user.Token = JWT.CreateJwt(user);
            //var newAccessToken = user.Token;
            //var newRefreshToken = CreateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            //await _authContext.SaveChangesAsync();
            
            return Ok(new 
            {
                User = user,
                Message = "User Logined!"
            });

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] TblUser userObj)
        {
            if (userObj == null)
                return BadRequest();
            // check userId
            // check username
            if (await _checkUsrObj.CheckUserNameExistAsync(userObj.UserName))
                return BadRequest(new { Message = "UserName Already Exist!" });

            // check email
            if (await _checkUsrObj.CheckEmailExistAsync(userObj.UsrEmail))
                return BadRequest(new { Message = "Email Already Exist!" });
            //password strength check
            var pass = PasswordStrength.CheckPasswordStrength(userObj.UsrPassword);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });
            userObj.UsrPassword = PasswordHasher.HashPassword(userObj.UsrPassword);
            userObj.UsrRole = "User";
            //userObj.UsrIsTenantAdmin = true;
            await _authContext.tblUsers.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            var user = await _authContext.tblUsers
               .FirstOrDefaultAsync(x => x.UserName == userObj.UserName);
            return Ok(new 
            {
                User = user,
                Message = "User Registered!"
            });

        }
        
       
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TblUser>> GetAlltblUsers()
        {
            return Ok(await _authContext.tblUsers.ToListAsync());
        }
    }
}

    

