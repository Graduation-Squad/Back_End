using Shipping.Core.Services.Contracts;
using Shipping_APIs.Attributes;
using System.Security.Claims;

namespace Shipping_APIs.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            var endpoint = context.GetEndpoint();
            var permissionAttribute = endpoint?.Metadata.GetMetadata<PermissionAttribute>();

            if (permissionAttribute != null)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = 401,
                        Message = "Unauthorized. User ID not found."
                    });
                    return;
                }

                var hasPermission = await userService.HasPermissionAsync(userId, permissionAttribute.Permission);
                if (!hasPermission)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = 403,
                        Message = "You do not have permission to access this resource.",
                        RequiredPermission = permissionAttribute.Permission
                    });

                    return;
                }
            }

            await _next(context);
        }
    }
}
