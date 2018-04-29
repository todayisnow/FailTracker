using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FailTracker.Web.App_Start;
using FailTracker.Web.Data;
using FailTracker.Web.Infrastructure;
using FailTracker.Web.Infrastructure.Tasks;
using FailTracker.Web.Migrations;


namespace FailTracker.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            var nestedContainer = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer;
            foreach (var task in nestedContainer.GetAllInstances<IRunAtInit>())
            {
                task.Execute();
            }

            foreach (var task in nestedContainer.GetAllInstances<IRunAtStartup>())
            {
                task.Execute();
            }
        }

        
    }
}
