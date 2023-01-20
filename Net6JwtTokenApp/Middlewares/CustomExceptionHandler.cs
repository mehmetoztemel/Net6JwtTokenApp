using System.Net.Mime;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Net6JwtTokenApp.Models;
using System.Text.Json;

namespace Net6JwtTokenApp.Middlewares
{
    public static class CustomExceptionHandler
    {

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    //context.Response.ContentType = "application/json";   

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    if (contextFeature != null)
                    {
                        var response = CustomResponse.Error((int)HttpStatusCode.InternalServerError,contextFeature.Error.Message);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
