using System;
using System.Data;
using SM.Configuration.Configuration;
using SM.Contracts.Models;
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