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
            var actionSplit = context.ActionDescriptor.RouteValues["action"].Split("_");
            if(actionSplit.Length==2)
            {
                string action = actionSplit[1];
                if (action.Contains("User"))
                {
                    if (context.HttpContext.Items["userId"] != null)
                    {
                        id = (long)context.HttpContext.Items["userId"];
                    }
                }
                if (action.Contains("Order") || action.Contains("Station") || action.Contains("OrderStation"))
                {
                    if (context.HttpContext.Items["stationId"] != null)
                    {
                        id = (long)context.HttpContext.Items["stationId"];
                    }
                }
                if (action.Contains("Deliverer"))
                {
                    if (context.HttpContext.Items["delivererId"] != null)
                    {
                        id = (long)context.HttpContext.Items["delivererId"];
                    }
                }
                if (action.Contains("Owner"))
                {
                    if (context.HttpContext.Items["ownerId"] != null)
                    {
                        id = (long)context.HttpContext.Items["ownerId"];
                    }
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
