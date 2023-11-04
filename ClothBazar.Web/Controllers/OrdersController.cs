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
using ClothBazar.Entities;
using ClothBaza.Services;
using System.Collections.Generic;
using ClothBazar.Entities.VM;
using Antlr.Runtime.Tree;
using ClothBazar.Database.Models;

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
            if (!User.IsInRole("Admin"))
            {
                var userName = User.Identity.Name;
                string userId = _eDb.Users.Where(x => x.Email == userName).FirstOrDefault().Id;

                vm.orderList = OrderService.Instance.GetList().Where(x => x.userId == userId).ToList();
            }
            else
            {
                vm.orderList = OrderService.Instance.GetList().ToList();
            }
            
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult statusPost(int paymentId , int id)
        {
            OrderVM vm = new OrderVM();
            
            bool updatedStatus = OrderService.Instance.UpdateStatus(id,paymentId);
            if (updatedStatus)
            {
                TempData["success"] = "Successfully Updated";
            }
            else
            {
                TempData["error"] = "Error";
            }
            return RedirectToAction("Index");
        }
    }
}