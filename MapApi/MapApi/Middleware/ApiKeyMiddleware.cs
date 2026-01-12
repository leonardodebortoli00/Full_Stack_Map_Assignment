namespace MapApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/map"))
            {
                string? apiKey = context.Request.Headers["X-Api-Key"].FirstOrDefault();
                if (apiKey != "FS_ReadWrite" && apiKey != "FS_Read")
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                if (context.Request.Path.Equals("/api/map/SetMap", StringComparison.OrdinalIgnoreCase) && apiKey != "FS_ReadWrite")
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }
            }
            await _next(context);
        }
    }
}
