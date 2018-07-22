using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class ProtectFileMiddleware
    {
        private readonly RequestDelegate next;

        public ProtectFileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/storage/protected"))
            {
                var userId = context.Session.GetString("UserId");
                if ("1234".Equals(userId) == false)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("File Access Denied", Encoding.UTF8);
                    return;
                }
            }
            await next(context);
        }
    }
}