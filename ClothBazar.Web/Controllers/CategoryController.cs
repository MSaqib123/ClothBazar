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
    public class CategoryController : Controller
    {
        //CategoriesService service = new CategoriesService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList(string search, int? pageNo)
        {
            //___________________ Pagination ____________________
            CategorySearchVM vm = new CategorySearchVM();

            vm.TotalPages = Math.Ceiling(CategoriesService.Instance.GetList().Count / 5.0);
            int totalPage = Convert.ToInt32(vm.TotalPages);
            vm.pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? (pageNo.Value > totalPage ? totalPage : pageNo.Value) : 1) : 1;
            vm.CategoryList = CategoriesService.Instance.GetList(vm.pageNo.Value);

            if (search != null && search != "")
            {
                vm.SearchTerms = search;
                vm.CategoryList = CategoriesService.Instance.SearchRecords(search);
            }
            return PartialView(vm);
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