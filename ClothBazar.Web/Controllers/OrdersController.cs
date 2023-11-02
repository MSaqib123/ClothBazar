using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClothBazar.Web.Models;
using ClothBazar.Entities;
using ClothBaza.Services;
using System.Collections.Generic;

namespace ClothBazar.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        ApplicationDbContext _eDb = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            OrderVM vm = new OrderVM();

            var userName = User.Identity.Name;
            string userId = _eDb.Users.Where(x => x.Email == userName).FirstOrDefault().Id;

            vm.orderList = OrderService.Instance.GetList().Where(x=>x.userId == userId).ToList();
            return View(vm);
        }
    }
}