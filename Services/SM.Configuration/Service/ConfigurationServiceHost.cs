using System;
using System.Data;
using SM.Core.Services;

namespace SM.Configuration.Service
{
    public class ConfigurationServiceHost : SMServiceHost
    {
        public override void OnStartUp()
        {
            var c = MainDB.MainDB.Connection;
        }
    }
}