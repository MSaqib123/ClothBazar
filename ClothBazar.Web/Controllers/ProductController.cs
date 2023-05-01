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

        //_____ Partial List + Saerch ________
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

        //____ Parital create _______
        public ActionResult Create()
        {
            //return View();
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Product model)
        {
            service.Save(model);
            return RedirectToAction("ProductList");
        }


        //____ Parital Edit _______
        public ActionResult Edit(int id)
        {
            var record = service.GetSingleRecord(id);
            return PartialView(record);
        }
        [HttpPost]
        public ActionResult Edit(Product model)
        {
            service.Update(model);
            return RedirectToAction("ProductList");
        }

        //____ Delete _______
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var record = service.GetSingleRecord(id);
            service.Delete(record);
            return RedirectToAction("ProductList");
        }
    }
}