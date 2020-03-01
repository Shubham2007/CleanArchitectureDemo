using Core.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace API.Filters
{
    /// <summary>
    /// Used to handle uncaught exception
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Entry point of uncaught exception handling
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HandleExceptionAsync(context);
            context.ExceptionHandled = true;
        }

        private void HandleExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            SetExceptionResult(context, exception, 
                exception is BaseException ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError);
        }

        private void SetExceptionResult(
            ExceptionContext context,
            Exception exception,
            HttpStatusCode statusCode)
        {
            _logger.LogError($"Exception Occured: {exception.Message}");

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new { ErrorMessage = exception.Message});
        }
    }
}
