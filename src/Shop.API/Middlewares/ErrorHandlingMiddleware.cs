﻿using Shop.Domain.Exceptions;

namespace Shop.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);  
            } 
            catch(ExistingUserException exception)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(exception.Message);
            }
            catch(NotFoundException exception)
            {
                logger.LogWarning(exception.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(exception.Message);
            }
            catch(ForbiddenException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            catch(ProductUnavailableException)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync("Product is unavailable");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}
