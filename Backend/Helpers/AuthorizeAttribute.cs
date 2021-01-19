using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Backend.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var id = 0L;
            var controller = context.ActionDescriptor.RouteValues["action"];
            if (controller.Contains("User"))
            {
                if (context.HttpContext.Items["userId"] != null)
                {
                    id = (long)context.HttpContext.Items["userId"];
                }
            }
            else if (controller.Contains("Order") || controller.Contains("Station") || controller.Contains("OrderStation"))
            {
                if (context.HttpContext.Items["stationId"] != null)
                {
                    id = (long)context.HttpContext.Items["stationId"];
                }
            }
            else if (controller.Contains("Deliver"))
            {
                if (context.HttpContext.Items["delivererId"] != null)
                {
                    id = (long)context.HttpContext.Items["delivererId"];
                }
            }
            else if (controller.Contains("Owner"))
            {
                if (context.HttpContext.Items["ownerId"] != null)
                {
                    id = (long)context.HttpContext.Items["ownerId"];
                }
            }
            if (id <= 0)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
