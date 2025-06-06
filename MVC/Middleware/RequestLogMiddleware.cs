﻿using Microsoft.AspNetCore.Http.Extensions;
using MVC.Data;
using MVC.Models;
using System.Globalization;

namespace MVC.Middleware;

public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ApplicationDbContext _db;
    //private readonly HttpContext _httpContext;

    public RequestLogMiddleware(RequestDelegate next)
    {
        _next = next;
        //_db = db;
        //_httpContext = httpContext;
    }

    

    public async Task InvokeAsync(HttpContext httpContext, ApplicationDbContext db)
    {
        var ip = httpContext.Connection.RemoteIpAddress.ToString();
        var url = httpContext.Request.GetDisplayUrl();

        var log = new RequestLog()
        {
            Date = DateTime.Now,
            IpAddress = ip,
            Url = url,
        };

        db.RequestLogs.Add(log);
        db.SaveChanges();

        // Call the next delegate/middleware in the pipeline.
        await _next(httpContext);
    }
}

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLog(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLogMiddleware>();
    }
}
