using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI
{
    /*
     * This class is the entry point of application.
     * the ASP.NET Core Web Application initially starts as a Console Application and the Main() method is the entry point to the application. 
     * So, when we execute the ASP.NET Core Web application, first it looks for the Main() method and this is the method from where the execution starts.
     * The Main() method then configures ASP.NET Core and starts it. 
     * At this point, the application becomes an ASP.NET Core web application.
     */
    public class Program
    {
        /*
         * Within the Main() method, on this IHostBuilder object, the Build() method is called which actually builds a web host. 
         * Then it hosts our asp.net core web application within that Web Host. 
         * Finally, on the web host, it called the Run()method, which will actually run the web application and
         * it starts listening to the incoming HTTP requests.
         * CreateDefaultBuilder() method sets up a web host with default configurations
         */
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /*
         * The Host is a static class that can be used for creating an instance of IHostBuilder with pre-configured defaults.
         * The CreateDefaultBuilder() method creates a new instance of the HostBuilder with pre-configured defaults. 
         * Internally, it configures Kestrel (Internal Web Server for ASP.NET Core), IISIntegration, and other configurations.
         * A. CreateDefaultBuilder()---> It does following:
         *      1. Setting up the webserver (will discuss in this article)
         *      2. Loading the host and application configuration from various configuration sources 
         *      3. Configuring logging 
         *   The Default Hosting model is InProcess
         */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        /*let’s rename the wwwroot folder to the “MyWebRoot” folder. Once you rename the wwwroot folder to MyWebRoot, 
        * then you need to call the UseWebRoot() method to configure MyWebRoot folder as a webroot folder in the Main() method of Program class as shown below.
        * 
        */
       /* Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
            webBuilder.UseStartup<Startup>().UseWebRoot("MyWebRoot");
        });*/
    }
}
