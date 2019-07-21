using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestBussiness.Connection;
using TestBussiness.Manager;
using TestBussiness.ManagerService;
using TestBussiness.Repository;
using TestBussiness.RepositoryService;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using StructureMap;
using TestBussiness.ServiceMessage.Builders;
using TestBussiness.ServiceMessage.Responses;
using TestBussiness.ServiceMessage.Responses.Factories;

namespace TestWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public class ControllerActionResponseServiceRegistry : Registry
        {
            public ControllerActionResponseServiceRegistry()
            {
                For(typeof(IControllerActionItemResponseFactory<,>)).Use(typeof(ControllerActionItemResponseFactory<,>));
                For(typeof(IControllerActionListResponseFactory<,>)).Use(typeof(ControllerActionListResponseFactory<,>));
            }
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container(_ =>
            {
                _.Scan(s =>
                {
                    s.Assembly("TestBussiness");
                    s.WithDefaultConventions();
                    s.ConnectImplementationsToTypesClosing(typeof(IControllerActionItemResponse<>));
                    s.ConnectImplementationsToTypesClosing(typeof(IControllerActionListResponse<>));
                    s.ConnectImplementationsToTypesClosing(typeof(IDtoBuilder<,>));
                });
                _.AddRegistry<ControllerActionResponseServiceRegistry>();
                _.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
