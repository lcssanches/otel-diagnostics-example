using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestDiagnostics
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private ILoggerFactory loggerFactory;
        private TracerProvider? tracerProvider;
        private MeterProvider? meterProvider;
        protected void Application_Start()
        {
            StartTelemetry();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void StartTelemetry()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["APPLICATIONINSIGHTS_CONNECTION_STRING"])) return;


            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddOpenTelemetry(logging =>
                {
                    logging.AddConsoleExporter();
                });
            });


            tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddAspNetInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation(x =>
                {
                    x.SetDbStatementForStoredProcedure = true;
                    x.SetDbStatementForText = true;
                }).AddConsoleExporter()
                .Build();
            
            meterProvider = Sdk.CreateMeterProviderBuilder()
                .AddAspNetInstrumentation()
                .AddHttpClientInstrumentation()
                .AddConsoleExporter()
                .Build();
        }

    }
}
