using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Middlewares
{
    public class GlobalExceptionHandlingMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddlewares> _logger;

        public GlobalExceptionHandlingMiddlewares(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddlewares> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // 404 Product not found throw ex , 500 internal server error
                if(context.Response.StatusCode == StatusCodes.Status404NotFound) // url is false (not ex)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something Went Wrong ==> : {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context) //URL NOT FOUND
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The endpoint with url {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)//ITEM NOT FOUND
        {
            //1] Change StatusCode
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                (_) => StatusCodes.Status500InternalServerError
            };
            //2] Change Content Type
            context.Response.ContentType = "application/json";

            //3] Write response in body
            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            }.ToString();
            await context.Response.WriteAsync(response);
        }
    }
}
