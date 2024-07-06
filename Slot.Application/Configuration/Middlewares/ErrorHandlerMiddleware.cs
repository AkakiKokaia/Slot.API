using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Shared.Configuration.Wrappers;
using System.Net;

namespace Slot.Application.Configuration.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException error)
        {
            await WrapResult((int)error.Code, error.Message ?? nameof(ApiException));
        }
        catch (System.ComponentModel.DataAnnotations.ValidationException error)
        {
            await WrapResult((int)ApiExceptionCodeTypes.Unhandled, error.Message);
        }
        catch (Slot.Application.Configuration.Exceptions.ValidationException error)
        {
            await WrapResult((int)ApiExceptionCodeTypes.Validation, JsonConvert.SerializeObject(error.ValidationErrors));
        }
        catch (Exception error)
        {
            await WrapResult((int)ApiExceptionCodeTypes.Unhandled, error.Message);
        }

        async Task WrapResult(int? errorCode, string? message)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<int?>(errorCode, message);

            response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(responseModel);

            _logger.Error(result);

            await response.WriteAsync(result);
        }
    }
}