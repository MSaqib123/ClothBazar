using ClothBaza.Services;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService service = new ProductsService();
        // GET: Product

        public ActionResult Index()
        {
            return View();
        }

        //_____ Partial Search ________
        public ActionResult ProductList(string search)
        {
            var list = service.GetList();
            if (search != null && search != "")
            {
                list = service.SearchRecords(search);
            }
            //return View(list);
            return PartialView(list);
        }



        //_______________  Create ________________
        //Just for adding record  ----> yaa bhi  ----->  Ajax krnaa ha  agaaa
        public ActionResult Create()
        {
            //return View();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product model)
        {
            service.Save(model);
            return RedirectToAction("Index");
        }
    }
}