using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AdSite.Extensions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CountryMiddleware
    {
        private string COUNTRY_ID = "CountryId";

        private readonly RequestDelegate _next;
        private readonly Guid _countryId;

        public CountryMiddleware(RequestDelegate next, Guid countryId)
        {
            _next = next;
            _countryId = countryId;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Items[COUNTRY_ID] = _countryId;
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CountryMiddlewareExtensions
    {
        public static IApplicationBuilder UseCountryMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CountryMiddleware>();
        }
    }
}
