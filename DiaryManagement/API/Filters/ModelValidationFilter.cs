using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;

namespace API.Filters
{
    /// <summary>
    /// Filter used to send error response in case of validation failure
    /// </summary>
    public class ModelValidationFilter : ActionFilterAttribute
    {
        #region Fields
        private readonly ILogger<ModelValidationFilter> _logger;
        #endregion

        #region Constructors

        public ModelValidationFilter(ILogger<ModelValidationFilter> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Properties
        #endregion

        #region Public Methods

        /// <summary>
        /// This methods gets executed every time when model binding take place in API controller
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                HandleValidationError(context);
                //context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Handling the validation error by setting the proper response with error message
        /// </summary>
        /// <param name="context"></param>
        private void HandleValidationError(ActionExecutingContext context)
        {
            string errorMessage = context.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;

            _logger.LogError($"Validation Error: { errorMessage}");

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new { ErrorMessage = errorMessage });
        }
        #endregion
    }
}
