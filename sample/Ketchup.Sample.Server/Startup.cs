﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Ketchup.Core;
using Ketchup.Core.Configurations;
using Ketchup.Core.Utilities;
using Ketchup.Grpc.Internal.Intercept;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ketchup.Sample.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfig.Configuration = (IConfigurationRoot)configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add things to the service collection.
            services.AddGrpc(grpc => grpc.Interceptors.Add<HystrixCommandIntercept>());

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.AddCoreService().RegisterModules();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Set up the application for development.

            ServiceLocator.Current = app.ApplicationServices.GetAutofacRoot();
            app.UseRouting();
            app.UseKetchup();
        }
    }
}
