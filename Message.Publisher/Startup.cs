using MassTransit;
using Message.Publisher.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Message.Publisher.Services;

namespace Message.Publisher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IProductService, ProductService>();

            var busControl = Bus.Factory.CreateUsingRabbitMq(rabbitMqConfig =>
            {
                rabbitMqConfig.Host(new Uri("rabbitmq://localhost"), h =>
                {

                    h.Username("guest");
                    h.Password("guest");
                });

                rabbitMqConfig.ConfigureJsonSerializer(settings =>
                {
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    return settings;
                });
            });

            services.AddSingleton<IBus>(busControl);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
