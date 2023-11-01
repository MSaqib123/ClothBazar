using ClothBaza.Services;
using ClothBazar.Entities;
using ClothBazar.Web.Migrations;
using ClothBazar.Web.Models;
using ClothBazar.Web.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        //ProductsService ProductServices = new ProductsService();

        ApplicationDbContext _eDb = new ApplicationDbContext();
        public ActionResult Index(string searchTerm, int? minimumPrice,int? maximumPrice,int? categoryId, int? sortyBy = 1, int? pageNo=1 )
        {
            ShopVM VM = new ShopVM();
            pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? pageNo.Value : 1) : 1;
            int pageSize = ConfigurationService.Instance.PageSize();
            VM.FeaturedCategory = CategoriesService.Instance.GetFeaturedList();
            VM.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            VM.SortBy = sortyBy;
            VM.CategoryId = categoryId;
            VM.MinPrice = minimumPrice;
            VM.MaxPrice = maximumPrice;
            VM.searchTerm = searchTerm;

            VM.Products = ProductsService.Instance.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryId,sortyBy,pageNo, pageSize);

            var totalShopProduct = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortyBy);
            VM.Pager = new Pager(totalShopProduct, pageNo);

            return View(VM);
        }
        
        //___ PriceFilter , Pagination ,  categoryFilter ___
        public ActionResult filterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortyBy = 1, int? pageNo = 1)
        {
            ShopProductFilterVM VM = new ShopProductFilterVM();
            pageNo = pageNo.HasValue ? (pageNo.Value > 0 ? pageNo.Value : 1) : 1;
            int pageSize = ConfigurationService.Instance.PageSize();

            VM.SortBy = sortyBy;
            VM.CategoryId = categoryId;
            VM.MinPrice = minimumPrice;
            VM.MaxPrice = maximumPrice;
            VM.searchTerm = searchTerm;

            VM.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryId, sortyBy ,pageNo, pageSize);

            var totalShopProduct = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortyBy);
            VM.Pager = new Pager(totalShopProduct, pageNo,pageSize);
            return PartialView(VM);
        }

        //======================================
        // CheckOut 
        //======================================
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
                var s = ProductsService.Instance.ProductCheckOutRecord(IDs);
                vm.CartProducts = s;
            }
            return View(vm);
        }

        //======================================
        // CheckoutFinal
        //======================================
        [Authorize]
        [HttpGet]
        public ActionResult CheckoutFinal()
        {
            CheckoutVM vm = new CheckoutVM();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            var IDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
            if (IDs.Count() > 0)
            {
                if (CartProductsCookie != null)
                {
                    var s = ProductsService.Instance.ProductCheckOutRecord(IDs);
                    vm.CartProducts = s;
                    vm.TotalProduct = IDs.Count();
                    vm.NetAmount = s.Sum(x => x.Price);
                }
                return View(vm);
            }
            else
            {
                return RedirectToAction("Checkout");
            }
            
        }
        [HttpPost]
        public ActionResult CheckoutFinalPost(CheckoutVM vm, string CartProductsJson)
        {
            List<ProductCheckOoutVM> cartProducts = JsonConvert.DeserializeObject<List<ProductCheckOoutVM>>(CartProductsJson);

            //_____ 1st Add Order  _____
            Order order = new Order();
            order.Status = 1;
            order.TotalAmount = vm.NetAmount;
            order.TotalProducts = vm.TotalProduct;
            order.OrderDate = DateTime.Now;
            int orderId = OrderService.Instance.Save(order);

            //_____ 2nd UserOrder Info _____
            var userName = User.Identity.Name;
            string userId = _eDb.Users.Where(x=>x.Email == userName).FirstOrDefault().Id;

            OrderUserInfo orderUser = new OrderUserInfo();
            orderUser.userId = userId;
            orderUser.orderId = orderId;
            orderUser.userEmail = vm.userDetail.email;
            orderUser.phoneNumber = vm.userDetail.phoneNumber;
            orderUser.userAddress = vm.userDetail.address;
            orderUser.userName = vm.userDetail.userName;
            OrderUserService.Instance.Save(orderUser);

            //_____ 3rd UserItems _____
            foreach (var item in cartProducts)
            {
                OrderItem orderProduct = new OrderItem();
                orderProduct.OrderId = orderId;
                orderProduct.ProductId = item.Id;
                orderProduct.ProductQty = item.Qty;
                orderProduct.Price = item.Price;
                orderProduct.TotalPrice = item.TotalPrice;
                OrderItemService.Instance.Save(orderProduct);
            
            }
            //________ Remove Cookie _____________
            var cookie = new HttpCookie("CartProducts");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index","Home");
        }
    }
}