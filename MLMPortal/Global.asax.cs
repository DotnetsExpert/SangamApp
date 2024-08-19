using Hangfire;
using MLMPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MLMPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Define the connection string
            //string connectionString = "Data Source=103.131.196.139;Initial Catalog=MissionPaymlmdb;User Id=mlmuser;Password=Varcas@mlm@2024";

            // Configure Hangfire
            //GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

            // Optional: Configure Hangfire to use a different job activation method if needed
            // GlobalConfiguration.Configuration.UseActivator(new MyJobActivator());

            // Start Hangfire server
            //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
            //Hangfire.BackgroundJobServerOptions serverOptions = new Hangfire.BackgroundJobServerOptions();
            //Hangfire.BackgroundJobServer server = new Hangfire.BackgroundJobServer(serverOptions);

            //// Schedule a daily recurring job
            //RecurringJob.AddOrUpdate(() => MyBackgroundJob.DailyTask(), Cron.Daily);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
