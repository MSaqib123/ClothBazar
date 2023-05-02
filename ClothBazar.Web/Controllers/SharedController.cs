using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        //__________ Upload __________
        public JsonResult UploadImage()
        {
            JsonResult result =new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Photo/"),fileName);
                file.SaveAs(path);

                result.Data = new { Success = true, Image = fileName, ImageURL =  string.Format("/Content/Photo/{0}",fileName) };

            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }

        //__________ Edit _ Delete Old __________
        public JsonResult DeleteImage(string imageURL)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var fileName = Path.GetFileName(imageURL);
                var path = Path.Combine(Server.MapPath("~/Content/Photo/"), fileName);
                System.IO.File.Delete(path);

                result.Data = new { Success = true };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }

    }
}