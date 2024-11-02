namespace MyWealth.WebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        // For simpler use in program.cs
        public static IApplicationBuilder UseMaintenanceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenanceMiddleware>();
        }
    }
}
