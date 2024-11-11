using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Book_Management.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse();

            // Handle 400 Bad Request
            if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Title = "Bad Request";
                response.ExceptionMessage = "The request could not be understood by the server due to malformed syntax or invalid data.";

            }


            // Handle 401 Unauthorized
            else if (context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Title = "Unauthorized";
                response.ExceptionMessage = "Authentication is required to access this resource.";
            }


            // Handle 404 Not Found
            else if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Title = "Not Found";
                response.ExceptionMessage = "The requested resource could not be found.";
            }



            // Handle 405 Method Not Allowed
            else if (context.HttpContext.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
            {
                response.StatusCode = StatusCodes.Status405MethodNotAllowed;
                response.Title = "Method Not Allowed";
                response.ExceptionMessage = "The HTTP method is not allowed for the requested resource.";
            }


            // Handle 200 OK (for successful GET, PUT, or PATCH requests)
            else if (context.HttpContext.Response.StatusCode == StatusCodes.Status200OK)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.Title = "OK";
                response.ExceptionMessage = "The request was successful.";
            }


            // Handle 201 Created (for successful POST requests that create a new resource)
            else if (context.HttpContext.Response.StatusCode == StatusCodes.Status201Created)
            {
                response.StatusCode = StatusCodes.Status201Created;
                response.Title = "Created";
                response.ExceptionMessage = "The resource was successfully created.";
            }



            // Handle other exceptions (default to 500 Internal Server Error)
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Title = "An error occurred while processing your request.";
                response.ExceptionMessage = context.Exception.Message;
            }

            // Set the result to return the JSON response
            context.Result = new JsonResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}