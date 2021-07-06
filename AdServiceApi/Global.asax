
<%@ Application Language="C#" %>

<script runat="server">
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdServiceApi.Models;
using System.Data.Entity;

namespace AdServiceApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new BookDbInitializer());
 
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
</script>