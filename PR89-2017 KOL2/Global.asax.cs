﻿using PR89_2017_KOL2.Helpers;
using PR89_2017_KOL2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;



namespace PR89_2017_KOL2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            HttpContext.Current.Application["korisnici"] = CitanjePodataka.citajKorisnike();
            HttpContext.Current.Application["vozila"] = CitanjePodataka.citajVozila();
            
        }
    }
}
