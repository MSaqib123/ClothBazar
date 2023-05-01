using ClothBaza.Services;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService service = new CategoriesService();

        public ActionResult Index()
        {
            var list = service.GetList();
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            service.Save(model);
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var record = service.GetSingleRecord(id);
            return View(record);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            service.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var record = service.GetSingleRecord(id);
            return View(record);
        }

        public ActionResult Delete(int id)
        {
            var record = service.GetSingleRecord(id);
            return View(record);
        }
        [HttpPost]
        public ActionResult Delete(Category model)
        {
            service.Delete(model);
            return RedirectToAction("Index");
        }


        

    }
}