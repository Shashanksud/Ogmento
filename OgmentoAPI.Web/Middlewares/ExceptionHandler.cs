using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.Extensions.Logging;
using OgmentoAPI.Domain.Common.Abstractions;
using OgmentoAPI.Domain.Common.Abstractions.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OgmentoAPI.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        public record ExceptionResponse(string exceptionType, HttpStatusCode StatusCode, string Description);
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, $"An unexpected error occurred. Request Details: {context.Request.Path}, QueryString: {context.Request.QueryString.ToString()}, " +
                $"Method: {context.Request.Method}, User: {context.User?.FindFirst(c=>c.Type == CustomClaimTypes.UserName)?.Value ?? "Anonymous"}");

            ExceptionResponse response = exception switch
            {
				DatabaseOperationException _ => new ExceptionResponse(nameof(DatabaseOperationException),HttpStatusCode.InternalServerError, exception.Message),
				EntityNotFoundException _ => new ExceptionResponse(nameof(EntityNotFoundException), HttpStatusCode.NotFound, exception.Message),
				InvalidOperationException _ => new ExceptionResponse(nameof(InvalidOperationException), HttpStatusCode.BadRequest, exception.Message),
				InvalidDataException _ => new ExceptionResponse(nameof(InvalidDataException), HttpStatusCode.BadRequest,exception.Message),
                ApplicationException _ => new ExceptionResponse(nameof(ApplicationException), HttpStatusCode.BadRequest, "Application exception occurred."),
                KeyNotFoundException _ => new ExceptionResponse(nameof(KeyNotFoundException), HttpStatusCode.NotFound, exception.Message),
                UnauthorizedAccessException _ => new ExceptionResponse(nameof(UnauthorizedAccessException), HttpStatusCode.Unauthorized, "Unauthorized user."),
                _ => new ExceptionResponse("InternalException", HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
			
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

