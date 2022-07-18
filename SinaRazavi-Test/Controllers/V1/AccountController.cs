using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Application.AccountApplication.Services;
using Swashbuckle.AspNetCore.Annotations;
using SinaRazavi_Test.Filters;

namespace SinaRazavi_Test.Controllers.V1
{
    [ApiVersion("1")]
    [ApiResultFilter]
    public class AccountController : BaseV1Controller
    {

        protected readonly IAcountService AcountService;
        public AccountController(IAcountService acountService)
        {
            AcountService = acountService;
        }

        [HttpPost("[action]")]
        [SwaggerOperation("Register user")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var result = await AcountService.RegisterUser(registerRequest, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return NoContent();
        }

        [HttpPost("[action]")]
        [SwaggerOperation("Get token")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var result = await AcountService.Login(loginRequest, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result.Data);
        }


    }
}
