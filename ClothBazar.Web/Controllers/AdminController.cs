using ClothBaza.Services;
using ClothBazar.Entities;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            //if(User.IsInRole("Admin"))
            //{ 
                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Orders");
            //}
        }

    }
}