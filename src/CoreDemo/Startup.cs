using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CoreDemo.Services;

namespace CoreDemo
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(@"c:\Users\me\documents\visual studio 2015\Projects\CoreDemo\src\CoreDemo\appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);
            services.AddSingleton<IGreeter, Greeter>();  //dependency injection
            services.AddMvc();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,             
            IGreeter greeter,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();            
            
            if (env.IsDevelopment())
            {
                //middleware 1
                app.UseDeveloperExceptionPage();
            }

            //must come after static files
            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            //combine usedefault files and use static files
            app.UseFileServer();

            app.UseMvcWithDefaultRoute();

            //app.UseWelcomePage();            

            //middleware RUN - terminal piece of middleware- won't call another middleware
            //buck stops here!
            //RUN middleware is rare, mostly USE middleware
            app.Run(async (context) =>
            {
                //throw new System.Exception("Error here!");
                //var greeting = Configuration["greeting"];
                var greeting = greeter.GetGreeting();
                //await context.Response.WriteAsync("Hello World and Beyond!!!");
                await context.Response.WriteAsync(greeting);
            });
        }
    }
}
