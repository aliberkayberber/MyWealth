namespace MyWealth.WebApi.Middlewares
{
    public static class ConfigureExceptionMiddleware
    {
        // For simpler use in program.cs
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
