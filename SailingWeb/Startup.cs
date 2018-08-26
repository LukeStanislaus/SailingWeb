using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Humanizer.Configuration;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using RazorPagesContacts.Data;

namespace SailingWeb
{
    public class Startup
    {
        private IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                  options.UseInMemoryDatabase("name"));
            services.AddMvc();

            var physicalProvider = _env.ContentRootFileProvider;
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
