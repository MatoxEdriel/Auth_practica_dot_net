using Application.DTOs;
using Intercore.shared.Response;
using MassTransit.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Api.shared;

public class GlobalResponseFilter:IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var statusCode = objectResult.StatusCode ?? 200;
            var isSuccess = statusCode >= 200 && statusCode < 300;

            var responseWrapper = new ApiResponse
            {
                Success = isSuccess,
                Message = isSuccess ? "Operación procesada correctamente." : "Hubo un problema con la petición.",
                    
                Data = isSuccess ? objectResult.Value : null,
                Errors = isSuccess ? null : objectResult.Value
            };

            objectResult.Value = responseWrapper;
        }

        await next();
    }
    
}