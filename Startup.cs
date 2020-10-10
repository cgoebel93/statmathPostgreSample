using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using statmathPostgreSample.Database;
using Microsoft.EntityFrameworkCore;

namespace statmathPostgreSample
{
    public class Startup
    {
        public Startup()
        {

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Model>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Model")));
        }

    }
    
}
