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
        //CategoriesService service = new CategoriesService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList(string search)
        {
            var list = CategoriesService.Instance.GetList();
            if (search != "" && search!=null )
            {
                list = CategoriesService.Instance.SearchRecords(search);
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
            CategoriesService.Instance.Save(model);
            return RedirectToAction("CategoryList");
        }


        public ActionResult Edit(int id)
        {
            var record = CategoriesService.Instance.GetSingleRecord(id);
            return PartialView(record);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            CategoriesService.Instance.Update(model);
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
            var record = CategoriesService.Instance.GetSingleRecord(id);
            CategoriesService.Instance.Delete(record);
            return RedirectToAction("CategoryList");
        }


        

    }
}