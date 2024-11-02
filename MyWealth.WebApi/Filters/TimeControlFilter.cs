using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MyWealth.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // Allows adding stocks between 06:00 - 23:59
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var now = DateTime.Now.TimeOfDay;

            StartTime = "06:00";
            EndTime = "23:59";

            if (now >= TimeSpan.Parse(StartTime) && now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuted(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "Requests cannot be sent to an endpoint between these hours.",
                    StatusCode = 403
                };
            }


        }
    }
}
