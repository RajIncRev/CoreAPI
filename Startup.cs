using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /*
         * access the configuration information within the Startup class, you need to use the IConfiguration service which is provided by the ASP.NET Core Framework. 
         * So, what you need to do is just inject the IConfiguration service through the constructor of the Startup class.
         */
        // Here we are using Dependency Injection to inject the Configuration object
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // The ConfigureServices()method of the Startup class configures the services which are required by the application.
        /*
          * The ConfigureServices method is a place where you can register your dependent classes with the built-in IoC container. 
          * After registering the dependent class, it can be used anywhere in the application. 
          * You just need to include it in the parameter of the constructor of a class where you want to use it. 
          * The IoC container will inject it automatically.
          * 
          * The ConfigureServices method includes the IServiceCollection parameter to register services to the IoC container. 
          * For example, if you want to add RazorPages services or MVC services to your asp.net core application, 
          * then you need to add those services to the parameter 
          * Example : services.AddMVC();
          *           services.AddRazor();
          *           services.AddController();.
          */
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /*
         * endpoints.MapControllers() --> Map to controllers
         */
        /*
         * ASP.NET Core introduced the middleware components to define a request pipeline, which will be executed on every request.
         * You include only those middleware components which are required by your application and thus increase the performance of your application.
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*   app.Use(async (context, next) =>
               {
                   await context.Response.WriteAsync("Middleware1: Incoming Request\n");
                   await next();
                   await context.Response.WriteAsync("Middleware1: Outgoing Response\n");
               });
               app.Use(async (context, next) =>
               {
                   await context.Response.WriteAsync("Middleware2: Incoming Request\n");
                   await next();
                   await context.Response.WriteAsync("Middleware2: Outgoing Response\n");
               });
               app.Run(async (context) =>
               {
                   await context.Response.WriteAsync("Middleware3: Incoming Request handled and response generated\n");
               });*/
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Specify the MyCustomPage.html as the default page
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("MyCustomPage1.html");

            //Setting the Default Files. If you will give http://localhost:<port> then it will load the index.html file
            app.UseDefaultFiles();
            //Adding Static Files Middleware to serve the static files.
            //This middleware is for displaying static files from wwwwroot folder.. If its not there we can't access the statuc files
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Worker Process Name : " + System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                    /*
                     * The default orders in which the various configuration sources are read for the same key are as follows
                        appsettings.json, 
                        appsettings.{Environment}.json here we use appsettings.development.json
                        User secrets
                        Environment variables
                        Command-line arguments
                     */
                    //Get the application configuration nformation
                    
                    await context.Response.WriteAsync(Configuration["MyCustomKey"]);
                });
            });
           
        }
    }
}
