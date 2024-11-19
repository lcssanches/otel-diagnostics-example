using System;
using System.IO;
using System.Web.Http;

namespace TestDiagnostics.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet()]
        public object Index()
        {
            //var a = new ContentResult()
            //{
            //    Content = System.Text.Json.JsonSerializer.Serialize(new
            //    {
            //        CurrentDirectory = AppContext.BaseDirectory,
            //        DiagnosticFileExists = System.IO.File.Exists("OTEL_DIAGNOSTICS.json")
            //    }),
            //    ContentType = "application/json"
            //};

            return Json(new
            {
                AppContext_BaseDirectory = AppContext.BaseDirectory,
                AppContext_BaseDirectory_FileExists = File.Exists(Path.Combine(AppContext.BaseDirectory,"OTEL_DIAGNOSTICS.json")),
                Directory_GetCurrentDirectory = Directory.GetCurrentDirectory(),
                Directory_GetCurrentDirectory_FileExists = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "OTEL_DIAGNOSTICS.json")),
                FileExists = File.Exists("OTEL_DIAGNOSTICS.json"),
            });
        }
    }
}
