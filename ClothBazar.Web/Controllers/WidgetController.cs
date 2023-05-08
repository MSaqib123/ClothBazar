using ClothBaza.Services;
using ClothBazar.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WidgetController : Controller
    {
        // GET: Widget
        public ActionResult Products(bool isLatestProducts)
        {
            ProductsWidgetVM vm = new ProductsWidgetVM();
            vm.isLatestProduct = isLatestProducts;
            if (isLatestProducts)
            {
                vm.ProductsList = ProductsService.Instance.GetLatestList(4);
            }
            else
            {
                vm.ProductsList = ProductsService.Instance.GetLatestList(1,8);
            }
            return PartialView(vm);
        }

    }
}