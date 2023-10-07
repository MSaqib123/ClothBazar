using ClothBaza.Services;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        //ProductsService ProductServices = new ProductsService();
        public ActionResult Index(string searchTerm, int? minimumPrice,int? maximumPrice,int? categoryId, int? sortyBy)
        {
            ShopVM VM = new ShopVM();
            VM.FeaturedCategory = CategoriesService.Instance.GetFeaturedList();
            VM.MaximumPrice = ProductsService.Instance.GetMaximumPrice();
            VM.Products = ProductsService.Instance.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryId,sortyBy);
            return View(VM);

            //___________________ Pagination ____________________
            //CategorySearchVM vm = new CategorySearchVM();
            ////vm.CategoryList = CategoriesService.Instance.GetList();
            //pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? pageNo.Value : 1) : 1;
            //vm.CategoryList = CategoriesService.Instance.GetList(search, pageNo.Value);

            //___ count Total Records _____
            //vm.SearchTerms = search;
            //var totalPages = CategoriesService.Instance.GetListCount(search);

            //if (vm.CategoryList != null)
            //{
            //    var paginationSize = ConfigurationService.Instance.GetConfig("paginationSize").Value;
            //    vm.Pager = new Pager(totalPages, pageNo, Convert.ToInt32(paginationSize));
            //    return PartialView(vm);
            //}
            //return HttpNotFound();
        }

        //____________ CheckOut _______________
        public ActionResult Checkout()
        {
            CheckoutVM vm = new CheckoutVM();

            //Getting Product from JQuery Cookie
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null)
            {
                //_____ 1. Ids ______
                // "5-5-5-5-7-7-8"  are in string form
                //var ProductIDs = CartProductsCookie.Value;

                //_____ 2. Remove  (-) ____
                //var ids = ProductIDs.Split('-');  // "5" , "5"    still string form
                //List<int> pIds = ids.Select(x => int.Parse(x)).ToList();

                //_________ Single Step ________
                var IDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                //_________ create services __________
                //service method jo  --> List Ids  laa ker ---> List Product return kraa
                var List = ProductsService.Instance.GetListRecordbyListIds(IDs);
                vm.CartProducts = List;
                // for Product Quentitys
                vm.CartProductsID = IDs;
            }
            return View(vm);
        }

    }
}