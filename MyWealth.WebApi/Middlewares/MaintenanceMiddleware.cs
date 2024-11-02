using MyWealth.Business.Operations.Setting;

namespace MyWealth.WebApi.Middlewares
{
    public class MaintenanceMiddleware
    {

        private readonly RequestDelegate _next;


        public MaintenanceMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();

            bool mainteneneceMode = settingService.GetMaintenanceState();

            // Login and maintenance mode without blocks all operations.
            if (context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/settings"))
            {
                await _next(context);
                return;
            }


            if (mainteneneceMode)
            {
                await context.Response.WriteAsync("Our site is under maintenance.");
            }

            else
            {
                await _next(context);
            }




        }
    }
}
