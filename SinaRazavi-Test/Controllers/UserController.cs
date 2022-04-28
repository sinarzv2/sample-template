using System.Linq;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.JwtServices;
using AutoMapper;
using Domain.Common;
using Domain.Common.Constant;
using Domain.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SinaRazavi_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected  UserManager<User> UserManager { get; }
        protected  IJwtService JwtService { get; }
        protected  IMapper Mapper { get; }
        public UserController(UserManager<User> userManager, IJwtService jwtService, IMapper mapper)
        {
            UserManager = userManager;
            JwtService = jwtService;
            Mapper = mapper;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var result = new ApiResult<UserDto>();
            var user = Mapper.Map<User>(userDto);
            var identityResult = await UserManager.CreateAsync(user, userDto.Password);
            if (!identityResult.Succeeded)
            {
                var allError = identityResult.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return BadRequest(result);
            }
            var resultUserRole = await UserManager.AddToRoleAsync(user,ConstantRoles.User);
            if (!resultUserRole.Succeeded)
            {
                var allError =  resultUserRole.Errors.Select(d => d.Description);
                result.AddErrors(allError);
                return BadRequest(result);
            }
            result.Success(userDto);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = new ApiResult<string>();
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                result.AddError("نام کاربری و رمز عبور اشتباه است.");
                return BadRequest(result);
            }
               
            var isPassValid = await UserManager.CheckPasswordAsync(user, password);
            if (!isPassValid)
            {
                result.AddError("نام کاربری و رمز عبور اشتباه است.");
                return BadRequest(result);
            }
            result.Data = await JwtService.GenerateAsync(user);
            return Ok(result);
        }
    }
}
