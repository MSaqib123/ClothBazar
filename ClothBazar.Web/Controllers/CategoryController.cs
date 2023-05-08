using ClothBaza.Services;
using ClothBazar.Entities;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            //vm.CategoryList = CategoriesService.Instance.GetList();
            pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? pageNo.Value : 1) : 1;
            vm.CategoryList = CategoriesService.Instance.GetList(search,pageNo.Value);

            //___ count Total Records _____
            vm.SearchTerms = search;
            var totalPages = CategoriesService.Instance.GetListCount(search);
            
            if (vm.CategoryList != null)
            {
                var paginationSize = ConfigurationService.Instance.GetConfig("paginationSize").Value;
                vm.Pager = new Pager(totalPages, pageNo, Convert.ToInt32(paginationSize));
                return PartialView(vm);
            }
            return HttpNotFound();
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