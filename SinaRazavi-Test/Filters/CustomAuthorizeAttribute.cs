using System;
using System.Linq;
using Common.Constant;
using Common.Models;
using Common.Resources.Messages;
using Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SinaRazavi_Test.Filters
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;
        public CustomAuthorizeAttribute(string permission)
        {
            _permission = permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity;
            if (identity.GetUserId() == null)
            {
                var apiResult = new ApiResult();
                apiResult.AddError(Errors.YouAreNotLoggedIn);
                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            var isPermitted = identity.FindClaimByType(ConstantPolicy.Permission).Any(d => d.Value == _permission);
            if (isPermitted) return;
            
            {
                var apiResult = new ApiResult();
                apiResult.AddError(Errors.AuthorizationFaild);
                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
