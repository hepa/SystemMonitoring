using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using SM.Configuration.Configuration;
using SM.Core.Services;

namespace SM.API.Service
{
    public class ApiServiceHost : SMServiceHost
    {
        IDisposable _server;

        public override void OnStartUp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("API is started.");
            var baseAddress = string.Format(@"http://localhost:{0}", SMConfigurations.Current.ApiConfiguration.PortNumber);
            _server = WebApp.Start<Startup>(baseAddress);
        }

        public override void OnStop()
        {
            _server.Dispose();
        }
    }
}