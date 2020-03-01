using API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace API.Extensions
{
    /// <summary>
    /// IApplicationBuilder Extensions
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Extension method to use custom exception middleware
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        /// <summary>
        /// Extension method to configure swagger
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
