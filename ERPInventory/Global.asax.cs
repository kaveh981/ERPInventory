using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ERPInventory
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // to initialize database for the first time 
            var context = new ERPInventoryDBContext();
            context.Database.Initialize(true);

            var result = context.Database.SqlQuery<Category>("GetParentCategoryByID @ID",
                new SqlParameter("ID", Guid.Parse("53097d2d-a537-e511-b770-002433726434"))
                ).ToList();
           int c = result.Count();
           string s = result.FirstOrDefault().Cat_Title;
        }
    }
}
