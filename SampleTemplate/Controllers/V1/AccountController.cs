using System;
using System.Threading;
using System.Threading.Tasks;
using Application.AccountApplication.Dto;
using Application.AccountApplication.Services;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleTemplate.Filters;
using Swashbuckle.AspNetCore.Annotations;

namespace SampleTemplate.Controllers.V1
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

        [HttpPost("[action]")]
        [SwaggerOperation("Refresh Token")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await AcountService.Refresh(request, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]/{userid}")]
        [CustomAuthorize("Account.Revoke")]
        [SwaggerOperation("Revoke Token")]
        public async Task<IActionResult> Revoke([FromRoute]Guid userid, CancellationToken cancellationToken)
        {
            var result = await AcountService.Revoke(userid, cancellationToken);
            if (!result.IsSuccess)
                return BadRequest(result);
            return NoContent();
        }
    }
}
