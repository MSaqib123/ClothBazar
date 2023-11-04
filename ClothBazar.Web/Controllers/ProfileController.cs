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
using ClothBaza.Services;
using ClothBazar.Entities;
using ClothBazar.Entities.VM;
using ClothBazar.Database.Models;

namespace ClothBazar.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        ApplicationDbContext _eDb = new ApplicationDbContext();
        [Authorize]
        public ActionResult Index()
        {
            userVM vm = new userVM();
            var userName = User.Identity.Name;
            var user = _eDb.Users.Where(x => x.Email == userName).FirstOrDefault();
            vm.userName = user.UserName;
            vm.email = user.Email;
            vm.phoneNumber = user.PhoneNumber;
            return View(vm);
        }

    }
}