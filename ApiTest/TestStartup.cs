using System;
using System.Collections.Generic;
using System.Text;
using WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IWebHostEnvironment currentEnvironment) : base(configuration, currentEnvironment)
        {
        }

        public override void ConfigureDependencies(IServiceCollection services)
        {
            base.ConfigureDependencies(services);
        }
    }
}
