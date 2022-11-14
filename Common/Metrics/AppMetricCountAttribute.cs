using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Common.Metrics
{
    public class AppMetricCountAttribute : ActionFilterAttribute
    {
        private readonly string metricname;
        public IMetricRegistry metricRegistry { get; set; }
        public AppMetricCountAttribute(string MetricName) => metricname = MetricName;                 

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            metricRegistry = (IMetricRegistry)context.HttpContext.RequestServices.GetService(typeof(IMetricRegistry));
            metricRegistry.IncrementCount(metricname);
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
