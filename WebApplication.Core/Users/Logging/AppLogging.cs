using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WebApplication.Core.Users.Logging
{
    public class AppLogging<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
       private readonly ILogger<AppLogging<TRequest,TRequest>> _logger;
   
        public AppLogging(ILogger<AppLogging<TRequest, TRequest>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string reqName = typeof(TRequest).Name;
            string reqId= Guid.NewGuid().ToString();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            var response =await next();
            stopWatch.Stop();
            _logger.LogInformation($"Name:{reqName},Time Taken:{stopWatch.ElapsedMilliseconds}");
            return response;
        }
    }
}
