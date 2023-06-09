﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClothBazar.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            ////______ Custom URL for   Search_Category ___________
            //routes.MapRoute(
            //    name: "AllCategories",
            //    url: "search/all",//"categories/all",
            //    defaults: new { controller = "Category", action = "CategoryList" }
            //);
        }
    }
}
