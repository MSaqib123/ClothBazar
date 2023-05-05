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
    public class ProductController : Controller
    {
        //ProductsService service = new ProductsService();
        // GET: Product

        public ActionResult Index()
        {
            return View();
        }

        //_____ Partial List + Saerch ________
        public ActionResult ProductList(string search)
        {
            var list = ProductsService.Instance.GetList();
            if (search != null && search != "")
            {
                list = ProductsService.Instance.SearchRecords(search);
            }
            //return View(list);
            return PartialView(list);
        }

        //____ Parital create _______
        public ActionResult Create()
        {
            //return View();
            //CategoriesService category = new CategoriesService();
            var list = CategoriesService.Instance.GetList();
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult Create(Product model)
        {
            ProductsService.Instance.Save(model);
            return RedirectToAction("ProductList");
        }


        //____ Parital Edit _______
        public ActionResult Edit(int id)
        {
            ProductVM vm = new ProductVM();

            vm.Product = ProductsService.Instance.GetSingleRecord(id);
            //CategoriesService category = new CategoriesService();
            vm.categoryList = CategoriesService.Instance.GetList();

            return PartialView(vm);
        }
        [HttpPost]
        public ActionResult Edit(Product model)
        {
            //var product = new Product();
            //product.Id = model.Product.Id;
            //product.Name = model.Product.Name;
            //product.Price = model.Product.Price;
            //product.CategoryId = model.Product.CategoryId;
            //product.Description = model.Product.Description;

            ProductsService.Instance.Update(model);
            return RedirectToAction("ProductList");
        }

        //____ Delete _______
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var record = ProductsService.Instance.GetSingleRecord(id);
            ProductsService.Instance.Delete(record);
            return RedirectToAction("ProductList");
        }
    }
}