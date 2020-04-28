using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookingSite.Web.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.Configuration;

namespace BookingSite.Web.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Обработка некорректных запросов.
        /// </summary>
        /// <returns>представление с результатом ошибки.</returns>
        public IActionResult HandleError()
        {
            var statusCode = HttpContext?.Response?.StatusCode ?? 0;

            var statusCodeDescription = $"{(HttpStatusCode)statusCode}";

            var statusFeature = HttpContext?.Features?.Get<IStatusCodeReExecuteFeature>();

            var target = HttpContext?.Features
                                    ?.Get<IHttpRequestFeature>()
                                    ?.RawTarget ?? $"{statusFeature?.OriginalPath}{statusFeature?.OriginalQueryString}";

            var modelStatusCode = statusFeature is null
                                      ? (int)HttpStatusCode.NotFound
                                      : statusCode;

            var modelStatusCodeDescription = statusFeature is null
                                                 ? $"{HttpStatusCode.NotFound}"
                                                 : $"{statusCodeDescription}";

            Log.Logger = SerilogConfiguration();
            Log.Fatal($"{modelStatusCode}", $"{modelStatusCodeDescription}");
            Log.CloseAndFlush();

            var message = string.Empty;
            if(statusCode == 500)
                message = "Во время работы приложения произошла непредвиденная ошибка. Пожалуйста, обратитесь к Администратору.";
            else if (statusCode == 404)
                message = "Такая страница не существует. Пожалуйста, обратитесь к Администратору.";
            else if (statusCode == 403)
                message = "Доступ запрещен. Пожалуйста, обратитесь к Администратору.";

            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier,
                Message = message,
                StatusCode = (statusCode, statusCodeDescription),
            };

            return View( model);
        }

        private static Logger SerilogConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .Build();

            var serilogConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return serilogConfig;
        }
    }
}