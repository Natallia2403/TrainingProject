using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TrainingProject.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
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

            _logger.LogWarning(target, $"{statusCode} ({statusCodeDescription})");

            var modelStatusCode = statusFeature is null
                                      ? (int)HttpStatusCode.NotFound
                                      : statusCode;

            var modelStatusCodeDescription = statusFeature is null
                                                 ? $"{HttpStatusCode.NotFound}"
                                                 : $"{statusCodeDescription}";

            //var model = new ErrorViewModel
            //{
            //    RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier,
            //    Message = AppSystemErrorMessages.IncorrectRequest,
            //    StatusCode = (modelStatusCode, modelStatusCodeDescription),
            //};

            //return View(model);
            return View();
        }
    }
}