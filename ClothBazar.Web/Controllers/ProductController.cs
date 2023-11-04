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
    //[Authorize(Roles ="Admin")]
    
    public class ProductController : Controller
    {
        //ProductsService service = new ProductsService();
        // GET: Product

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        //_____ Partial List + Saerch ________
        [Authorize]
        public ActionResult ProductList(string search,int? pageNo)
        {
            //___________________ Pagination ____________________
            ProductSearchVM vm = new ProductSearchVM();
            
            vm.TotalPages = Math.Ceiling(ProductsService.Instance.GetList().Count / 5.0);
            int totalPage = Convert.ToInt32(vm.TotalPages);
            vm.pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? (pageNo.Value > totalPage ? totalPage : pageNo.Value ) : 1) : 1;
            vm.ProductsList = ProductsService.Instance.GetList(vm.pageNo.Value);

            if (search != null && search != "")
            {
                vm.SearchTerms = search;
                vm.ProductsList = ProductsService.Instance.SearchRecords(search);
            }
            return PartialView(vm);
        }

        //____ Parital create _______
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ProductVM vm = new ProductVM();

            vm.Product = ProductsService.Instance.GetSingleRecord(id);
            //CategoriesService category = new CategoriesService();
            vm.categoryList = CategoriesService.Instance.GetList();

            return PartialView(vm);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var record = ProductsService.Instance.GetSingleRecord(id);
            ProductsService.Instance.Delete(record);
            return RedirectToAction("ProductList");
        }

        //========================================================
        // Detail Product 
        //========================================================
        public ActionResult ProductDetails(int id)
        {
            ProductDetailVM vm = new ProductDetailVM();
            vm.Product = ProductsService.Instance.GetSingleRecord(id);
            return View(vm);
        }

    }
}