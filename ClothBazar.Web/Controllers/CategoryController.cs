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
            return View();
        }

        public ActionResult CategoryList(string search)
        {
            var list = service.GetList();
            if (search != "" && search!=null )
            {
                list = service.SearchRecords(search);
            }
            return PartialView(list);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category model)
        {
            service.Save(model);
            return RedirectToAction("CategoryList");
        }


        public ActionResult Edit(int id)
        {
            var record = service.GetSingleRecord(id);
            return PartialView(record);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            service.Update(model);
            return RedirectToAction("CategoryList");
        }

        //public ActionResult Detail(int id)
        //{
        //    var record = service.GetSingleRecord(id);
        //    return View(record);
        //}

        //public ActionResult Delete(int id)
        //{
        //    var record = service.GetSingleRecord(id);
        //    return View(record);
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var record = service.GetSingleRecord(id);
            service.Delete(record);
            return RedirectToAction("CategoryList");
        }


        

    }
}