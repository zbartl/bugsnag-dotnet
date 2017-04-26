using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Bugsnag.NetCore.ConfigurationStorage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Bugsnag.NetCore.Clients
{
    public class WebMVCClient
    {
        public readonly Configuration Config;
        public readonly BaseClient Client;

        public WebMVCClient(BugsnagSettings bugsnagSettings, HttpContext context)
        {
            Client = new BaseClient(bugsnagSettings);
            Config = Client.Config;
            Client.Config.BeforeNotify(error =>
            {
                if (context != null && context.Request != null)
                {
                    var queryString = context.Request.QueryString;
                    if (queryString.HasValue)
                    {
                        error.Metadata.AddToTab("Request", "query-string", queryString.Value);
                    }

                    var cookies = context.Request.Cookies;
                    foreach(var cookie in cookies)
                    {
                        if (!string.IsNullOrEmpty(cookie.Value))
                        {
                            error.Metadata.AddToTab("Request", cookie.Key, cookie.Value);
                        }
                    }

                    if (String.IsNullOrEmpty(error.Context) && context.Request.Path != null)
                    {
                        error.Context = context.Request.Path.ToString();
                    }

                    if (String.IsNullOrEmpty(error.UserId))
                    {
                        if (!String.IsNullOrEmpty(context.User.Identity.Name))
                        {
                            error.UserId = context.User.Identity.Name;
                        }
                        else if (context.Session != null && !String.IsNullOrEmpty(context.Session.Id))
                        {
                            error.UserId = context.Session.Id;
                        }
                        else
                        {
                            error.UserId = context.Connection?.RemoteIpAddress?.ToString();
                        }
                    }

                    if (context.Request.Method == "POST")
                    {
                        var formParams = context.Request.Form;
                        foreach(var param in formParams)
                        {
                            if (!string.IsNullOrEmpty(param.Value.ToString()))
                            {
                                error.Metadata.AddToTab("Request", param.Key, param.Value);
                            }
                        }
                    }

                    var headers = context.Request.Headers;
                    foreach (var header in headers)
                    {
                        error.Metadata.AddToTab("Request", header.Key, header.Value);
                    }
                }
            });
        }

        public void Start()
        {

        }

        public async Task Notify(Exception error)
        {
            await Client.Notify(error);
        }

        public async Task Notify(Exception error, Metadata metadata)
        {
            await Client.Notify(error, metadata);
        }

        public async Task Notify(Exception error, Severity severity)
        {
            await Client.Notify(error, severity);
        }

        public async Task Notify(Exception error, Severity severity, Metadata metadata)
        {
            await Client.Notify(error, severity, metadata);
        }

        /// <summary>
        /// Exception attribute to automatically handle errors when registered (requires > .NET 4.0)
        /// </summary>
        public sealed class BugsnagExceptionHandler : IAsyncExceptionFilter
        {
            private readonly WebMVCClient _client;

            public BugsnagExceptionHandler(IHttpContextAccessor httpContextAccessor, IOptions<BugsnagSettings> bugsnagSettings)
            {
                _client = new WebMVCClient(bugsnagSettings.Value, httpContextAccessor.HttpContext);
            }

            public async Task OnExceptionAsync(ExceptionContext filterContext)
            {
                if (filterContext == null || filterContext.Exception == null)
                    return;

                if (_client.Config.AutoNotify)
                    await _client.Client.Notify(filterContext.Exception, Severity.Error);
            }
        }
    }
}
