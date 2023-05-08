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
        public ActionResult Products(bool isLatestProducts , int? CategoryId = 0)
        {
            ProductsWidgetVM vm = new ProductsWidgetVM();
            vm.isLatestProduct = isLatestProducts;
            if (isLatestProducts)
            {
                vm.ProductsList = ProductsService.Instance.GetLatestList(4);
            }
            else if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                vm.ProductsList = ProductsService.Instance.GetListByCategory(CategoryId.Value,4);
            }
            else
            {
                vm.ProductsList = ProductsService.Instance.GetLatestList(1,8);
            }
            return PartialView(vm);
        }
    }
}