using System;

namespace SM.Contracts.Models.Configuration
{
    public class ApiConfiguration
    {
        public string URL { get; set; } = "http://localhost";

        public int PortNumber { get; set; } = 9000;
    }
}
